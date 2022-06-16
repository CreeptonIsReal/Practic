using Microsoft.Win32;
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


namespace TestApp
{
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private bool YesLinkToVariant(object HandleAny)
    {

      if (String.IsNullOrEmpty(HandleAny.ToString()))
      {
        return false;
      }
      else { return true; }
    }

    private object ConnectTo1C8_COM(string s1cAppProgID, string InitLine) //Bred/Peredelat
    {
      var dialog = new System.Windows.Forms.FolderBrowserDialog();
      s1cAppProgID = s1cAppProgID.Trim();
      if (s1cAppProgID.Length <= 0)
      {
        s1cAppProgID = "V8.COMConnector";
      }

      InitLine = InitLine.Trim();
      if (InitLine.Length > 0)
      {
        Type Type1C = Type.GetTypeFromProgID(s1cAppProgID);
        dynamic C1Inst = Activator.CreateInstance(Type1C);
        C1Inst.Visible = true; //non CreateOleObject 
        //return Type1C;
        if (YesLinkToVariant(Type1C))
        {
          return Type1C + InitLine; //Bred non connect 
        }
        else
        {
          MessageBox.Show("Fail");
          //
        }
      }
      return InitLine;
    }

    private void Select1CButton_Click(object sender, RoutedEventArgs e)
    {
      object s1C8_ole;
      s1C8_ole = ConnectTo1C8_COM(NameCOMObject1C.Text, InitString.Text);

      if (YesLinkToVariant(s1C8_ole))
      {
        MessageBox.Show("COM +++");
      } else
      {
        MessageBox.Show("COM ---");
      }

    }
  }
}
