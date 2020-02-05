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

		public delegate void AddMessageDelegate(string message);

		public Form1()
		{
			InitializeComponent();
			bw = new BackgroundWorker();
			bw.DoWork += StartParse;
		}

		public void LogAdd(string message)
		{
			textBox.AppendText(message);
		}

		private void StartParse(object sender, DoWorkEventArgs e)
		{

			int unsuccessRecords = 0;

			// TODO: добавить диалог
			using (var reader = new StreamReader(Environment.CurrentDirectory + "\\Bittrex_ETHBTC_1h.csv"))
			using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
			{
				int i = 0;
				csv.Read();
				csv.ReadHeader();
				while (csv.Read())
				{
					
					Invoke(new AddMessageDelegate(LogAdd), new object[] { Environment.NewLine + i + " " });
					try
					{
						var currency = new Currency();

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

						IFormatProvider dateFormater = new CultureInfo("en-US");

						currency.DateTime = DateTime.ParseExact(dateStr, "yyyy-mm-dd hh-tt", CultureInfo.InvariantCulture).ToUniversalTime();

						currency.CurrencyName = pairName.Trim().Replace("BTC", "");


						if (currency.CurrencyName != "" && currency.DateTime != null
							&& currency.VolumeCurrency != 0m && currency.VolumeBtc != 0m
							&& currency.High != 0m && currency.Low != 0m
							&& currency.BuyPrice != 0m && currency.SellPrice != 0m)
						{
							Invoke(new AddMessageDelegate(LogAdd), new object[] { dateStr + " Success" });
							// TODO: запись в базу
						}
						else
						{
							Invoke(new AddMessageDelegate(LogAdd), new object[] { dateStr + " Unsuccess" });

							unsuccessRecords++;
						}
					}
					catch (Exception ex)
					{
						textBox.Text += ex.Message;
						unsuccessRecords++;
					}

					i++;
				}

				Invoke(new AddMessageDelegate(LogAdd), new object[] {
					Environment.NewLine + Environment.NewLine +
					"Всего записей: " + i + Environment.NewLine +
					"Неудачных записей: " + unsuccessRecords });

			}


		}

		private void buttonStart_Click(object sender, EventArgs e)
		{
			this.bw.RunWorkerAsync();

		}
	}
}
