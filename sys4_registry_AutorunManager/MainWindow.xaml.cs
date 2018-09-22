using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace sys4_registry_AutorunManager
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		RegistryKey actualRegistryKey;
		public MainWindow()
		{
			InitializeComponent();

			WindowStartupLocation = WindowStartupLocation.CenterScreen;

			Loaded += MainWindow_Loaded;
		}

		private void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			try
			{
				actualRegistryKey = Registry.LocalMachine
					.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
			
				foreach (var item in actualRegistryKey.GetValueNames())
				{
					myList.Items.Add(item);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		
		private void ButtonAdd_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Exe Files (.exe)|*.exe|All Files (*.*)|*.*";
			if(openFileDialog.ShowDialog() == true)
			{
				string shortFileName = System.IO.Path.GetFileNameWithoutExtension(openFileDialog.FileName);
				actualRegistryKey.SetValue(shortFileName, $"\"{openFileDialog.FileName}\"");
				myList.Items.Add(shortFileName);
			}
		}

		private void removeBtn_Click(object sender, RoutedEventArgs e)
		{
			if(myList.SelectedItems.Count>0)
			{
				actualRegistryKey.DeleteValue((string)myList.SelectedItem);
				myList.Items.Remove(myList.SelectedItem);
			}
		}
	}
}
