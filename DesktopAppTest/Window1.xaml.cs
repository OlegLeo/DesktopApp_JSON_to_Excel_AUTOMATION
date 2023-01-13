using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Collections.ObjectModel;
using System.Data;
using IronXL;



//using static DesktopAppTest.Accao;

namespace DesktopAppTest
{
    
    public class Accao
    {
        
       public string AccaoCODIGO { get; set; }
       public string Ano { get; set; }
       public string Designacao { get; set; }
       public DateTime DtFim { get; set; }
       public DateTime DtInicio { get; set; }
       public string LocalRealizacao { get; set; }
       public string Responsavel { get; set; }
       public double TotalHoras { get; set; }

       /* public Accao(string AccaoCODIGO, string Ano, string Designacao, string DtFim, string DtInicio, string LocalRealizacao, string Responsavel, double TotalHoras)
        {
            this.AccaoCODIGO = AccaoCODIGO;
            this.Ano = Ano;
            this.Designacao = Designacao;

            //this.DtFim = DateTime.Parse(DtFim);

            this.DtFim = DateTime.ParseExact(DtFim, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            this.DtInicio = DateTime.ParseExact(DtInicio, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            //this.DtInicio = DateTime.Parse(DtInicio);
            this.LocalRealizacao = LocalRealizacao;
            this.Responsavel = Responsavel;
            this.TotalHoras = TotalHoras;

        }*/
    }


    public class RootObjectAccao
    {
        public List<Accao> accao { get; set; }
    }



    public class Inscrito
    {
        public string Nome { get; set; }
        public string DocIdentNumero { get; set; }
        public DateTime DtNascimento { get; set; }
        public string Empresa { get; set; }
        public string EmpresaFuncao { get; set; }
        public string Formacao { get; set; }
    }

    public class RootObjectInscrito
    {
        public List<Inscrito> inscritos { get; set; }
    }







    /// <summary>
    /// Lógica interna para Window1.xaml
    /// </summary>
    /// 

    public partial class Window1 : Window
    {
        

        public Window1()
        {

            InitializeComponent();

            


            // --> Deserializing "accao" JSON file with Newtonsoft.Json <-- \\

            // Getting the path to inscritos.json file

            string filePathToAccao = "D:\\ESTAGIO_CESAE\\DesktopAppTest\\DesktopAppTest\\files\\accao.json";
            
                                     

            // Making the file readeble 
            string jsonAccao = File.ReadAllText(filePathToAccao);

            // Deserializing JSON file and binding with a Accao object class

            var objAccao = JsonConvert.DeserializeObject<RootObjectAccao>(jsonAccao);



            //var objAccao = JsonConvert.DeserializeObject<Accao>(jsonAccao);

            // --> // END of Deserializing "accao" JSON FILE // <-- \\



            // Creating a list that extracts all names of COURSES, for binding with interface  

            List<string> viewList = objAccao.accao.Select(i => i.Designacao).ToList();
            //List<string> viewList = new List<string> { objAccao.Designacao };





            // -- Deserializing "inscritos" JSON file with Newtonsoft.Json -- //



            // Getting the path to inscritos.json file

            string filePathToInscritos = "D:\\ESTAGIO_CESAE\\DesktopAppTest\\DesktopAppTest\\files\\inscritos.json";

            // Making the file readeble 
            string jsonInscritos = File.ReadAllText(filePathToInscritos);

            // Deserializing JSON file and binding with a object class

            RootObjectInscrito objInscritos = JsonConvert.DeserializeObject<RootObjectInscrito>(jsonInscritos);

            // --> // END of Deserializing "inscritos" JSON FILE // <-- \\





            // Code for displaying the data from a viewList on the user's interface, so that is possible to see all the names of Courses 

            ObservableCollection<string> oList;
            oList = new System.Collections.ObjectModel.ObservableCollection<string>(viewList);

            lista.DataContext = viewList;

            Binding binding = new Binding();
            lista.SetBinding(ListBox.ItemsSourceProperty, binding);


            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lista.ItemsSource);
            view.Filter = UserFilter;



            


        }

        // Search Bar/Filter code.

        private bool UserFilter(object item)
        {
            

            if (String.IsNullOrEmpty(txtNameSearch.Text))
                return true;
            else
                return (item.ToString().IndexOf(txtNameSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }


        private void txtFilter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lista.ItemsSource).Refresh();
        }



        // ON ITEM CLICK EVENT --> from a json file write the Excel file and store it on the computer. 
        
        private void lista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lista.SelectedItem != null)
            {

                // MessageBox.Show(lista.SelectedItem.ToString());


                // --> Deserializing "accao" JSON file with Newtonsoft.Json <-- \\

                // Getting the path to inscritos.json file

                string filePathToAccao = "D:\\ESTAGIO_CESAE\\DesktopAppTest\\DesktopAppTest\\files\\accao.json";

                // Making the file readeble 
                string jsonAccao = File.ReadAllText(filePathToAccao);

                // Deserializing JSON file and binding with a Accao object class

                //RootObjectAccao objAccao = JsonConvert.DeserializeObject<RootObjectAccao>(jsonAccao); <-------- THIS DESERIALIZATION doesnt work for what we want
                //var objAccao = JsonConvert.DeserializeObject<Accao>(jsonAccao);


                var objAccao = JsonConvert.DeserializeObject<dynamic>(jsonAccao);
                //List<Accao> accaoList = objAccao.accao;

                List<Accao> accaoList = objAccao.accao.ToObject<List<Accao>>();



                // --> // END of Deserializing "accao" JSON FILE // <-- \\



                /////////////////////////////////////////////////////////////////////

                // -- Deserializing "inscritos" JSON file with Newtonsoft.Json -- //



                // Getting the path to inscritos.json file

                string filePathToInscritos = "D:\\ESTAGIO_CESAE\\DesktopAppTest\\DesktopAppTest\\files\\inscritos.json";

                // Making the file readeble 
                string jsonInscritos = File.ReadAllText(filePathToInscritos);

                // Deserializing JSON file and binding with a object class

                //Accao objInscritos = JsonConvert.DeserializeObject<Accao>(jsonInscritos); <-------- THIS DESERIALIZATION doesnt work for what we want

                var objInscritos = JsonConvert.DeserializeObject<dynamic>(jsonInscritos);

                List<Inscrito> inscritosList = objInscritos.inscritos.ToObject<List<Inscrito>>();



                // --> // END of Deserializing "inscritos" JSON FILE // <-- \\




                // variables for storing data ----> accao
                var excelData_Course_Designacao = "";
                string excelData_DtInicio = "";
                string excelData_DtFim = "";
                var excelData_AccaoCODIGO = "";
                var excelData_Ano = "";
                var excelData_LocalRealizacao = "";
                var excelData_Responsavel = "";
                string excelData_TotalHoras = "";

               
   

                // Finding all object data by Course Name
                foreach (Accao item in accaoList)
                {
                    //MessageBox.Show(item.Course_Name);
                    if (item.Designacao == lista.SelectedItem.ToString())
                    {
                        // Returning values inside variables 
                        excelData_Course_Designacao = item.Designacao;
                        excelData_DtInicio = item.DtInicio.ToString();
                        excelData_DtFim = item.DtFim.ToString();
                        excelData_AccaoCODIGO = item.AccaoCODIGO;
                        excelData_Ano = item.Ano;
                        excelData_LocalRealizacao = item.LocalRealizacao;
                        excelData_Responsavel = item.Responsavel;
                        excelData_TotalHoras = item.TotalHoras.ToString();



                    }
                }


                



                // Opening the TEMPLATE of Excel File to Write it with new data

                WorkBook workbook = WorkBook.Load("D:\\ESTAGIO_CESAE\\DesktopAppTest\\DesktopAppTest\\files\\dtpfernave2023_v1.xls");//import Excel SpreadSheet

                // SHEET NAME ----> 0_FApres
                WorkSheet sheetAccao = workbook.GetWorkSheet("0_FApres");//access specific workshet

                // Option 1 of accessing and editing excel file
                //sheet.Rows[3].Columns[1].Value = "New Value";//access specific cell and modify its value

                // Option 2 of accessing and editing excel file 


                // Writing/Updating the tamplete Excel file with new data       ----> Worksheet name "0_FApres" <----

                //Course Name
                sheetAccao["B5"].Value = excelData_Course_Designacao;

                // Starting Date
                sheetAccao["B8"].Value = excelData_DtInicio;

                // Finish Date
                sheetAccao["B10"].Value = excelData_DtFim;

                //Course's number/code
                sheetAccao["G8"].Value = excelData_AccaoCODIGO;

                //Course's year
                //sheetAccao["??"].Value = excelData_Ano;    // Needs confirmation

                // Location
                sheetAccao["E10"].Value = excelData_LocalRealizacao;

                // Name of responable person
                sheetAccao["B18"].Value = excelData_Responsavel;

                // Total Course's Hours
                sheetAccao["I12"].Value = excelData_TotalHoras;




                // SHEET NAME ----> 5_FCR2
                WorkSheet sheetInscritos = workbook.GetWorkSheet("5_FCR2");//access specific workshet

                

                // Loop that fills the cells of each of people that are registered to participate on the course -> Name, ID number, Birth Date, etc...

                int i = 6;
                foreach (Inscrito item2 in inscritosList)
                {
                    if (lista.SelectedItem.ToString() == item2.Formacao)
                    {
                        sheetInscritos["C" + i].Value = item2.Nome;
                        sheetInscritos["E" + i].Value = item2.DocIdentNumero;
                        sheetInscritos["F" + i].Value = item2.DtNascimento;
                        sheetInscritos["G" + i].Value = item2.Empresa;
                        // sheetInscritos["?" + i].Value = item2.EmpresaFuncao;
                        i++;
                    }
                }




                // Configure save file dialog box (ASKING USER WHERE DOES HE WANT TO STORE THIS NEW EXCEL FILE)

                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = "Excel_Test"; // Default file name
                dlg.DefaultExt = ".xls"; // Default file extension
                dlg.Filter = "Text documents (.XLS)|*.XLS"; // Filter files by extension

                // Show save file dialog box
                Nullable<bool> result = dlg.ShowDialog();


                // Process save file dialog box results
                if (result == true)
                {
                    // Save document
                    string filename = dlg.FileName;
                    workbook.SaveAs(dlg.FileName);

                    MessageBox.Show("Documento guardado com sucesso!");

                }
                

            }
        }
        
        
    }
}










