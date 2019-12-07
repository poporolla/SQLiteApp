using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data.Entity;
using System.Runtime.CompilerServices;

namespace SQLiteApp
{
	class ApplicationViewModel : INotifyPropertyChanged
	{
		ApplicationContext db;
		RelayCommand addCommand;
		RelayCommand editCommand;
		RelayCommand deleteCommand;
		IEnumerable<Phone> phones;

		public IEnumerable<Phone> Phones
		{
			get
			{
				return phones;
			}
			set
			{
				phones = value;
				OnPropertyChanged("Phones");
			}
		}
		public ApplicationViewModel()
		{
			db = new ApplicationContext();
			db.Phones.Load();
			Phones = db.Phones.Local.ToBindingList();
		}
		public RelayCommand AddCommand
		{
			get
			{
				return addCommand ??
					(addCommand = new RelayCommand(obj =>
					{
						PhoneWindow phoneWindow = new PhoneWindow(new Phone());
						if (phoneWindow.ShowDialog() == true)
						{
							Phone phone = phoneWindow.Phone;
							db.Phones.Add(phone);
							db.SaveChanges();
						}
					}));
			}
		}
		public RelayCommand EditCommand
		{
			get
			{
				return editCommand ??
					(editCommand = new RelayCommand(selectedItem =>
					{
						if (selectedItem == null)
						{
							return;
						}
						Phone phone = selectedItem as Phone;

						Phone vm = new Phone()
						{
							Id = phone.Id,
							Title = phone.Title,
							Company = phone.Company,
							Price = phone.Price
						};
						PhoneWindow phoneWindow = new PhoneWindow(vm);

						if (phoneWindow.ShowDialog() == true)
						{
							phone = db.Phones.Find(phoneWindow.Phone.Id);
							if (phone != null)
							{
								phone.Company = phoneWindow.Phone.Company;
								phone.Title = phoneWindow.Phone.Title;
								phone.Price = phoneWindow.Phone.Price;
								db.Entry(phone).State = EntityState.Modified;
								db.SaveChanges();
							}
						}
					},
					selectedItem =>
					{
						return selectedItem != null;
					}));
			}
		}
		public RelayCommand DeleteCommand
		{
			get
			{
				return deleteCommand ??
					(deleteCommand = new RelayCommand(selectedItem =>
					{
						if (selectedItem == null)
						{
							return;
						}
						Phone phone = selectedItem as Phone;
						db.Phones.Remove(phone);
						db.SaveChanges();
					},
					selectedItem =>
					{
						return selectedItem != null;
					}));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string property = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
		}
	}
}
