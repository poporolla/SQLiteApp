using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SQLiteApp
{
	public class Phone : INotifyPropertyChanged
	{
		private string title;
		private string company;
		private int price;
		
		public int Id { get; set; }

		public string Title
		{
			get
			{
				return title;
			}
			set
			{
				title = value;
				OnPropertyChanged("Title");
			}
		}
		public string Company
		{
			get
			{
				return company;
			}
			set
			{
				company = value;
				OnPropertyChanged("Company");
			}
		}
		public int Price
		{
			get
			{
				return price;
			}
			set
			{
				price = value;
				OnPropertyChanged("Price");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}
