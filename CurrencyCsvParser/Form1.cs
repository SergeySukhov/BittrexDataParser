using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CsvHelper;

namespace CurrencyCsvParser
{
	public partial class Form1 : Form
	{
		BackgroundWorker bw;
        CurrencyProvider provider;
        public delegate void AddMessageDelegate(string message);

		public Form1()
		{
			InitializeComponent();
			bw = new BackgroundWorker();
			bw.DoWork += StartParse;
		}

        public void WriteLog(string message)
        {
            Invoke(new AddMessageDelegate(LogAdd), new object[] { message });
        }

        public void LogAdd(string message)
		{
			textBox.AppendText(message);
		}

		private void StartParse(object sender, DoWorkEventArgs e)
		{
            TaskFactory taskFactory = new TaskFactory();

            var sorceDirectory = @"..\..\..\ParseSourse";
            var sourceFiles = Directory.GetFiles(sorceDirectory);

            WriteLog(sourceFiles.Aggregate((x, y) =>  x + "|" + y));

            foreach(var file in sourceFiles)
            {
                if (file != "" && file.EndsWith(".csv"))
                {
                    taskFactory.StartNew(() =>
                    {
                       ParseCsv(file);
                    });
                }
            }

		}

        public void ParseCsv(string filePath)
        {
            var fileName = new FileInfo(filePath).Name;

            int unsuccessRecords = 0;
            
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var curList = new List<Currency>();

                int i = 0;
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    try
                    {

                        var currency = new Currency();
                        currency.Guid = Guid.NewGuid();

                        var forBuyStr = csv.GetField("Open");
                        var forSellStr = csv.GetField("Close");
                        var dateStr = csv.GetField("Date");
                        var highStr = csv.GetField("High");
                        var lowStr = csv.GetField("Low");
                        var pairName = csv.GetField("Symbol");
                        var volBtc = csv.GetField("Volume BTC");
                        var volCur = csv.GetField("Volume " + pairName.Trim().Replace("BTC", ""));

                        IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };

                        currency.BuyPrice = decimal.Parse(forBuyStr, formatter);
                        currency.SellPrice = decimal.Parse(forSellStr, formatter);

                        currency.High = decimal.Parse(highStr, formatter);
                        currency.Low = decimal.Parse(lowStr, formatter);

                        currency.VolumeBtc = decimal.Parse(volBtc, formatter);
                        currency.VolumeCurrency = decimal.Parse(volCur, formatter);


                        currency.DateTime = DateTime.ParseExact(dateStr, "yyyy-mm-dd hh-tt", CultureInfo.InvariantCulture).ToUniversalTime();

                        currency.CurrencyName = pairName.Trim().Replace("BTC", "");


                        if (currency.CurrencyName != "" && currency.DateTime != null
                            && currency.VolumeCurrency != 0m && currency.VolumeBtc != 0m
                            && currency.High != 0m && currency.Low != 0m
                            && currency.BuyPrice != 0m && currency.SellPrice != 0m)
                        {
                            curList.Add(currency);
                        }
                        else
                        {
                            WriteLog(Environment.NewLine + fileName + ": " + " Строка " + (i + 1) + "| " + dateStr + " Unsuccess");
                            unsuccessRecords++;
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        WriteLog(Environment.NewLine + Environment.NewLine + "Ошибка! " + ex.Message + Environment.NewLine + Environment.NewLine);
                        unsuccessRecords++;
                    }

                    i++;
                }
                WriteLog(Environment.NewLine + Environment.NewLine +
                    fileName + ": " +
                    "Всего записей: " + i + Environment.NewLine +
                    "Неудачных записей: " + unsuccessRecords
                    + Environment.NewLine + Environment.NewLine);

                provider.SaveCurrencyRecords(curList);

            }
        } 

		private void buttonStart_Click(object sender, EventArgs e)
		{
            provider = new CurrencyProvider();
			this.bw.RunWorkerAsync();

		}
	}
}
