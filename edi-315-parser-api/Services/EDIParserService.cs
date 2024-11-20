using edi_315_parser_api.Models;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;

namespace edi_315_parser_api.Services
{
    public class EDIParserService: IEDIParserService
    {
        private readonly Container _container;

        public EDIParserService(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task ParseEDIFile(IFormFile file)
        {
            List<string> lines = new List<string>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    lines.Add(await reader.ReadLineAsync());
            }

            var ediDataList = ParseEDI315(lines);

            foreach (var ediData in ediDataList) 
            { 
                // string jsonResult = JsonConvert.SerializeObject(ediData, Formatting.Indented);
                // Console.WriteLine(jsonResult);
                await _container.CreateItemAsync(ediData, new PartitionKey(ediData.PartitionKey));
            }
        }

        public async Task<EDI315Data> GetEDIDataByContainerNoAsync(string containerNo)
        {
            var query = new QueryDefinition("SELECT * FROM c WHERE c.TransectionSet.B4.container_number = @containerNo").WithParameter("@containerNo", containerNo);

            var options = new QueryRequestOptions
            {
                PartitionKey = new PartitionKey(containerNo)
            };

            using FeedIterator<EDI315Data> resultSet = _container.GetItemQueryIterator<EDI315Data>(query, requestOptions: options);

            if (resultSet.HasMoreResults)
            {
                var response = await resultSet.ReadNextAsync();
                return response.Resource.FirstOrDefault(); // Return the single matching document
            }

            return null; // No document found
        }

        private static List<EDI315Data> ParseEDI315(List<string> lines)
        {
            List<EDI315Data> ediDataList = new List<EDI315Data>();
            
            EDI315Data ediData = null;
            ISA isa = null;
            GS gs = null;
            TransectionSet transectionSet = null;

            foreach (var line in lines)
            {
                var elements = line.Split('*');
                switch (elements[0])
                {
                    case "ISA":
                        isa = new ISA
                        {
                            AuthorizationInformationQualifier = GetElement(elements, 1),
                            AuthorizationInformation = GetElement(elements, 2),
                            SecurityInformationQualifier = GetElement(elements, 3),
                            SecurityInformation = GetElement(elements, 4),
                            InterchangeIdQualifierSender = GetElement(elements, 5),
                            InterchangeSenderId = GetElement(elements, 6),
                            InterchangeIdQualifierReceiver = GetElement(elements, 7),
                            InterchangeReceiverId = GetElement(elements, 8),
                            InterchangeDateTime = GetTimestamp(elements, 9, 10),
                            InterchangeControlStandardsId = GetElement(elements, 11),
                            InterchangeControlVersionNumber = GetElement(elements, 12),
                            InterchangeControlNumber = GetElement(elements, 13),
                            AcknowledgmentRequested = GetElement(elements, 14),
                            UsageIndicator = GetElement(elements, 15)
                        };
                        break;
                    case "GS":
                        gs = new GS
                        {
                            FunctionalIdentifierCode = GetElement(elements, 1),
                            ApplicationReceiverCode = GetElement(elements, 3),
                            FunctionalGroupDateTime = GetTimestamp(elements, 4, 5),
                            GroupControlNumber = GetElement(elements, 6),
                            ResponsibleAgencyCode = GetElement(elements, 7),
                            VersionReleaseIndustryIdentifierCode = GetElement(elements, 8)
                        };
                        break;
                    case "ST":
                        transectionSet = new TransectionSet();

                        transectionSet.ST = new ST
                        {
                            TransactionSetIdentifierCode = GetElement(elements, 1),
                            TransactionSetControlNumber = GetElement(elements, 2)
                        };

                        if(transectionSet.ST.TransactionSetIdentifierCode != "315")
                        {
                            throw new Exception("Uploaded EDI file is not EDI 315 transaction");
                        }
                        break;
                    case "B4":
                        transectionSet.B4 = new B4
                        {
                            InquiryRequestNumber = GetElement(elements, 2),
                            ShipmentStatusCode = GetElement(elements, 3),
                            ShipmentStatusCodeDescription = GetShipmentStatusDescription(GetElement(elements, 3)),
                            InquiryRequestDateTime = GetTimestamp(elements, 4, 5),
                            ContainerNumber = $"{GetElement(elements, 7)}{GetElement(elements, 8)}",
                            EquipmentStatusCode = GetElement(elements, 9),
                            EquipmentStatusCodeDescription = GetEquipmentStatusDescription(GetElement(elements, 9)),
                            EquipmentType = GetElement(elements, 10),
                            EquipmentTypeDescription = GetEquipmentTypeDescription(GetElement(elements, 10).Substring(2, 2))
                        };
                        break;
                    case "N9":
                        var n9 = new N9
                        {
                            ReferenceQualifier = GetElement(elements, 1),
                            ReferenceQualifierDescription = GetReferenceDescription(GetElement(elements, 1)),
                            ReferenceIdentification = GetElement(elements, 2)
                        };
                        transectionSet.N9.Add(n9);
                        break;
                    case "Q2":
                        transectionSet.Q2 = new Q2
                        {
                            VesselCode = GetElement(elements, 1),
                            CountryCode = GetElement(elements, 2),
                            DepartureDate = GetTimestamp(elements, 4, 5),
                            LadingQuantity = GetElement(elements, 6),
                            Weight = GetElement(elements, 7),
                            WeightQualifier = GetWeightQualifier(GetElement(elements, 8)),
                            VoyageNumber = GetElement(elements, 9),
                            VesselCodeQualifier = GetVesselCodeQualifier(GetElement(elements, 12)),
                            VesselName = GetElement(elements, 13),
                            WeightUnitCode = GetWeightUnitCode(GetElement(elements, 16))
                        };
                        break;
                    case "SG":
                        var shipmentStatus = new ShipmentStatus
                        {
                            ShipmentStatusCode = GetElement(elements, 1),
                            ShipmentStatusCodeDescription = GetShipmentStatusDescription(GetElement(elements, 1)),
                            ShipmentDateTime = GetTimestamp(elements, 4, 5)
                        };
                        transectionSet.ShipmentStatus.Add(shipmentStatus);
                        break;
                    case "R4":
                        var portTerminalInfo = new PortTerminal
                        {
                            PortFunctionCode = GetElement(elements, 1),
                            PortFunctionCodeDescription = GetPortFunctionDescription(GetElement(elements, 1)),
                            LocationTypeCode = GetElement(elements, 2),
                            LocationTypeCodeDescription = GetLocationTypeCodeDescription(GetElement(elements, 2)),
                            LocationIdentifier = GetElement(elements, 3),
                            PortName = GetElement(elements, 4),
                            CountryCode = GetElement(elements, 5),
                            TerminalName = GetElement(elements, 6),
                            StateOrProvinceCode = GetElement(elements, 7)
                        };
                        transectionSet.PortTerminal.Add(portTerminalInfo);
                        break;
                    case "SE":
                        ediData = new EDI315Data();

                        ediData.PartitionKey = transectionSet.B4.ContainerNumber;
                        ediData.ISA = isa;
                        ediData.GS = gs;
                        ediData.TransectionSet = transectionSet;

                        ediDataList.Add(ediData);
                        transectionSet = null;
                        ediData = null;
                        break;
                    default:
                        break;
                }
            }

            return ediDataList;
        }

        private static string GetElement(string[] elements, int index)
        {
            var value = index < elements.Length ? elements[index]?.Trim() : null;
            return string.IsNullOrEmpty(value) || value == "00" || value == "ZZ" ? null : value;
        }

        private static string GetTimestamp(string[] elements, int dateIndex, int timeIndex)
        {
            var date = GetElement(elements, dateIndex);
            var time = GetElement(elements, timeIndex);

            if (!string.IsNullOrWhiteSpace(date) && date.Length == 6)
                date = $"20{date.Substring(0, 2)}-{date.Substring(2, 2)}-{date.Substring(4, 2)}";

            if (!string.IsNullOrWhiteSpace(date) && date.Length == 8)
                date = $"{date.Substring(0, 4)}-{date.Substring(4, 2)}-{date.Substring(6, 2)}";

            if (!string.IsNullOrWhiteSpace(time) && time.Length == 4)
                time = $"{time.Substring(0, 2)}:{time.Substring(2, 2)}:00";

            return !string.IsNullOrWhiteSpace(date) && !string.IsNullOrWhiteSpace(time)
                ? $"{date}T{time}"
                : null;
        }

        /*private static string GetShipmentStatusDescription(string code)
        {
            return code switch
            {
                "D" => "Completed Unloading at Delivery Location",
                "I" => "In-Gate",
                "AE" => "Loaded on Vessel",
                "AG" => "Change in Estimated Time of Arrival",
                "AL" => "Loaded on Rail",
                "AR" => "Rail Arrival at Destination Intermodal Ramp",
                "AV" => "Available for Delivery",
                "CU" => "Carrier and Customs Release",
                "EE" => "Empty Equipment Dispatched",
                "OA" => "Out-Gate",
                "RD" => "Return Container",
                "UV" => "Unloaded From Vessel",
                "VA" => "Vessel Arrival",
                "VD" => "Vessel Departure",
                _ => null
            };
        }*/

        private static string GetEquipmentStatusDescription(string code)
        {
            return code switch
            {
                "E" => "Empty",
                "L" => "Load",
                _ => null
            };
        }

        private static string GetEquipmentTypeDescription(string code)
        {
            return code switch
            {
                "G1" => "General Purpose Container",
                "R1" => "Refrigerated Container",
                "U1" => "Open Top Container",
                "P1" => "Platform Container",
                "T1" => "Tank Container",
                _ => null
            };
        }

        private static string GetReferenceDescription(string qualifier)
        {
            return qualifier switch
            {
                "SCA" => "Standard Carrier Alpha Code (SCAC)",
                "AAO" => "Carrier Assigned Code",
                "TI" => "Transaction Number",
                "GCD" => "Group Code",
                "SN" => "Seal Number",
                "ZCD" => "Zone Code",
                "BN" => "Booking Number",
                "BM" => "Bill of Lading Number",
                "YS" => "Yard Position",
                "EQ" => "Equipment Number",
                "L1" => "Letter Note",
                "TT" => "Terminal Code",
                "GC" => "PIN Number",
                "22" => "Special Charge or Allowance Code",
                "4I" => "Day 1 Demurrage",
                "4I1" => "Day 2 Demurrage",
                "4I2" => "Day 3 Demurrage",
                "4I3" => "Day 4 Demurrage",
                "4I4" => "Day 5 Demurrage",
                "4I5" => "Day 6 Demurrage",
                "4I6" => "Day 7 Demurrage",
                "4I7" => "Day 8 Demurrage",
                "4I8" => "Day 9 Demurrage",
                "4I9" => "Day 10 Demurrage",
                "4IV" => "Vacis Exam Fee",
                "4IE" => "Container Exam Fee",
                "TMF" => "Traffic Mitigation Fee",
                "CTF" => "Clean Truck Fee",
                "HZP" => "Placard Fee",
                "FCH" => "Flip Fee",
                "BBC" => "Break-bulk Fee",
                "DVF" => "Devanning Fee",
                "USD" => "USDA Fee",
                "GEN" => "Genset Plug_Unplug Fee",
                "SCR" => "Sealing Containers Fee",
                "WCR" => "Weighing Containers Fee",
                "GEC" => "Grounded Export Containers Fee",
                "DRC" => "Drayage of Container Fee",
                "ATG" => "Tailgate Inspection",
                "EGF" => "Export Gate Fee",
                "IGF" => "Import Gate Fee",
                "OOG" => "OOG Flip",
                "RF" => "Rehandling Fee",
                "DWT" => "Extended Dwell Time Fee",
                "FI" => "Fee Indicator Flag",
                _ => null
            };
        }

        private static string GetWeightQualifier(string code)
        {
            return code switch
            {
                "G" => "Gross Weight",
                _ => null
            };
        }

        private static string GetVesselCodeQualifier(string code)
        {
            return code switch
            {
                "C" => "Ship's Radio Call Signal",
                "L" => "Lloyd's Register of Shipping",
                _ => null
            };
        }

        private static string GetWeightUnitCode(string code)
        {
            return code switch
            {
                "K" => "Kilograms",
                "L" => "Pounds",
                _ => null
            };
        }

        private static string GetShipmentStatusDescription(string code)
        {
            return code switch
            {
                "A" => "Arrived",
                "AE" => "VESSE LLOAD",
                "AL" => "RAIL LOAD",
                "AV" => "Available For Delivery",
                "CB" => "Chassis Tie",
                "CR" => "Carrier Release",
                "CT" => "Customs Released",
                "CZ" => "Reefer",
                "DN" => "Container Not Available for pickup",
                "FD" => "Freight is Due",
                "FT" => "Free Time Expired (Demurrage)",
                "H1" => "Hazmat",
                "HR" => "Hold Released (CUSTOMS)",
                "I" => "GATE IN",
                "IB" => "US Customs In-bond Movement Authorized",
                "IR" => "Movement Changed from In-Bond to Not In-Bond",
                "LD" => "Late Delivery",
                "NF" => "Free Time To Expire",
                "NP" => "Terminal Charges Paid",
                "NT" => "Notify",
                "OA" => "GATE OUT",
                "OD" => "Over Dimension",
                "PA" => "Customs Hold, Intensive Examination",
                "PB" => "Customs Hold, Insufficient Paperwork",
                "PC" => "Customs Hold, Discrepancy In Paperwork",
                "PD" => "Customs Hold, Discrepancy In Piece Count",
                "PE" => "Customs Hold, Hold by Coast Guard",
                "PF" => "Customs Hold, Hold By FBI",
                "PG" => "Customs Hold, Hold By Local Law Enforcement",
                "PH" => "Customs Hold, Hold By Court Imposed Lien",
                "PI" => "Customs Hold, Hold By Food and Drug",
                "PJ" => "Customs Hold, Hold By Fish And Wildlife",
                "PK" => "Customs Hold, Hold By Drug Enforcement",
                "PL" => "Dept. Agr, Hold for Intensive Investigation",
                "PM" => "Dept. Agr, Hold for Unregistered Producer",
                "PN" => "Dept. Agr, Hold for Restricted Commodity",
                "PO" => "Dept. Agr, Hold for Insect Infestation",
                "PP" => "Dept. Agr, Hold for Bacterial Contamination",
                "PQ" => "Customs Hold at Place of Vessel Arrival",
                "PR" => "Customs Hold at In-Bond Destination",
                "PS" => "Dept Agr Hold at Place of Vessel Arrival",
                "PT" => "Dept Agr Hold at In-Bond Destination",
                "PU" => "Other Agency Hold at Place of Vessel Arrival",
                "PV" => "Other Agency Hold at In-Bond Destination",
                "PW" => "Dept Agr, Hold for Fumigation",
                "PX" => "Dept Agr, Hold for Inspection or Doc. Review",
                "RI" => "Movement Changed from Not In-Bond to In-Bond",
                "RL" => "Rail Departure from Origin Intermodal Ramp",
                "SA" => "Shipment Split (Devan)",
                "SB" => "Shipment Consolodation (Devan)",
                "TC" => "Held For Terminal Charges",
                "UR" => "RAIL UNLOAD",
                "UV" => "VESSEL UNLOAD",
                "VA" => "Vessel Arrival",
                _ => null
            };
        }

        private static string GetPortFunctionDescription(string code)
        {
            return code switch
            {
                "1" => "Final Port of Discharge",
                "5" => "Activity Location",
                "E" => "Place of Final Delivery",
                "L" => "Port of Loading",
                "R" => "Place of Receipt",
                "Y" => "Port of interchange / Relay Port",
                "D" => "Port of Discharge",
                _ => null
            };
        }

        private static string GetLocationTypeCodeDescription(string code)
        {
            return code switch
            {
                "D" => "Census Schedule D",
                "K" => "Census Schedule K",
                "CI" => "City",
                "UN" => "United Nations Location Code (UNLOCODE)",
                _ => null
            };
        }
    }
}
