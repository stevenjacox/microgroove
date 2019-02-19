using Microgroove.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Microgroove
{
    public class Utility
    {
        BatchFile CurrentBatchFile;
        List<Order> BatchFileOrders;

        public Utility()
        {
            //Create root object
            CurrentBatchFile = new BatchFile();

            //Initialize orders object
            List<Order> batchFileOrders = new List<Order>();
            CurrentBatchFile.Orders = batchFileOrders;
        }

        public List<List<string>> ReadAllLinesIntoAList(string fileLocation)
        {
            //Read all lines into a list of lines
            List<List<string>> lines = new List<List<string>>();
            using (StreamReader sr = new StreamReader(fileLocation))
            {
                string currentline;

                int index = 0;
                while ((currentline = sr.ReadLine()) != null)
                {
                    var splitline = currentline.Split(",").ToList();

                    List<string> newsplitline = new List<string>();

                    foreach (string part in splitline)
                    {
                        //Remove quotes here
                        string quotesremoved = part.Trim('"');
                        newsplitline.Add(quotesremoved);
                    }
                    //Save index number for each line for order info gathering.
                    newsplitline.Add(index.ToString());
                    lines.Add(newsplitline);
                    index++;
                }

            };
            return lines;
        }

        public string ProcessLines(List<List<string>> lines)
        {
            var currentFileInfoLine = lines.First();

            DateTime batchFileDateTime;
            DateTime.TryParse(currentFileInfoLine[1], out batchFileDateTime);

            CurrentBatchFile.BatchFileDate = batchFileDateTime;
            CurrentBatchFile.BatchFileType = currentFileInfoLine[2];

            //Find lines that are order lines
            var orderlines = lines.Where(p => p[0].Equals("O"));

            foreach (List<string> orderline in orderlines)
            {
                Order order = new Order();
                Buyer buyer = new Buyer();
                Timings timings = new Timings();
                LineItem lineItem = new LineItem();
                List<LineItem> lineItems = new List<LineItem>();
                int orderIndex = int.Parse(orderline.Last());

                DateTime orderDate;
                if (!DateTime.TryParse(orderline[1], out orderDate))
                {
                    throw new Exception("Date parsing error at order line number " + (orderIndex + 1));
                }

                //Take only the lines for the current order but not the ender
                var orderinfolines = lines.Skip(orderIndex + 1).TakeWhile(p => !p[0].Equals("O") && !p[0].Equals("E")).ToList();

                foreach (List<string> orderinfoline in orderinfolines)
                {
                    switch (orderinfoline.First())
                    {
                        case "B":
                            buyer.Name = orderinfoline[1];
                            buyer.Street = orderinfoline[2];
                            buyer.Zip = orderinfoline[3];
                            break;
                        case "L":
                            lineItem = new LineItem();
                            lineItem.LineItemSKU = orderinfoline[1];
                            lineItem.LineItemQuantity = int.Parse(orderinfoline[2].TrimStart('0'));
                            lineItems.Add(lineItem);
                            break;
                        case "T":
                            timings.Start = int.Parse(orderinfoline[1]);
                            timings.Stop = int.Parse(orderinfoline[2]);
                            timings.Gap = int.Parse(orderinfoline[3]);
                            timings.Offset = int.Parse(orderinfoline[4]);
                            timings.Pause = int.Parse(orderinfoline[5]);
                            break;
                        default:
                            var linenumber = int.Parse(orderinfoline.Last()) + 1;
                            throw new WrongLineTypeException("Wrong type of line at line number " + linenumber.ToString());

                    }

                }
                order.OrderBuyer = buyer;
                order.OrderLineItems = lineItems;
                order.OrderDate = orderDate;
                order.OrderCode = orderline[2];
                order.OrderNumber = orderline[3];
                order.OrderTimings = timings;
                CurrentBatchFile.Orders.Add(order);
            }

            var enderline = lines.Last();
            Ender ender = new Ender();

            ender.Process = int.Parse(enderline[1]);
            ender.Paid = int.Parse(enderline[2]);
            ender.Created = int.Parse(enderline[3]);

            CurrentBatchFile.FileEnder = ender;

            return JsonConvert.SerializeObject(CurrentBatchFile, Formatting.Indented);
        }
    }

    [Serializable]
    public class WrongLineTypeException : Exception
    {
        public WrongLineTypeException()
        {
        }

        public WrongLineTypeException(string message) : base(message)
        {
        }

        public WrongLineTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WrongLineTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
