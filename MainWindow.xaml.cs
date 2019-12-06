using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;

namespace SQLiteApp
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		ApplicationContext db;
		public MainWindow()
		{
			InitializeComponent();

			db = new ApplicationContext();
			db.Phones.Load();
			this.DataContext = db.Phones.Local.ToBindingList();
		}

		private void Add_Click(object sender, RoutedEventArgs e)
		{
			PhoneWindow phoneWindow = new PhoneWindow(new Phone());
			if (phoneWindow.ShowDialog() == true)
			{
				Phone phone = phoneWindow.Phone;
				db.Phones.Add(phone);
				db.SaveChanges();
			}
		}

		private void Edit_Click(object sender, RoutedEventArgs e)
		{
			if (phonesList.SelectedItem == null)
			{
				return;
			}

			Phone phone = phonesList.SelectedItem as Phone;

			PhoneWindow phoneWindow = new PhoneWindow(new Phone
			{
				Id = phone.Id,
				Company = phone.Company,
				Price = phone.Price,
				Title = phone.Title
			});

			if (phoneWindow.ShowDialog() == true)
			{
				phone = db.Phones.Find(phoneWindow.Phone.Id);
				if (phone != null)
				{
					phone.Company = phoneWindow.Phone.Company;
					phone.Price = phoneWindow.Phone.Price;
					phone.Title = phoneWindow.Phone.Title;
					db.Entry(phone).State = EntityState.Modified;
					db.SaveChanges();
				}
			}
		}

		private void Delete_Click(object sender, RoutedEventArgs e)
		{
			if (phonesList.SelectedItem == null)
			{
				return;
			}
			Phone phone = phonesList.SelectedItem as Phone;
			db.Phones.Remove(phone);
			db.SaveChanges();
		}
	}
}
