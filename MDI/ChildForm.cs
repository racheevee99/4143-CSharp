///////////////////////////////////////////////////////////////////////
//
// Name:            Rachel Vetter
// Professor:       Dr. Stringfellow
// Class:           CMPS 4143
// Project:         Program 7 - Inventory Program
// 
// Purpose: This program is an application that uses an MDI to 
// display inventory information from different businesses in 
// separate child form windows. This inventory information
// can be written in manually through the app or read
// directly to a serialized file and is then written to 
// a serialized file.
//
// Child Form: This form displays the current inventory information
// of a new or existing business from a file or from new input.
//
///////////////////////////////////////////////////////////////////////

using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;

namespace Program7App
{
    public partial class ChildForm : Form
    {
        //Path Property
        public string Path { get; set; }
        //List of records from serialized file
        static List<Record> records = new List<Record>();
        // object for deserializing Record in binary format 
        private BinaryFormatter reader = new BinaryFormatter(); 


        //Name: Record
        //Purpose: To hold serializable information about the
        //inventory of a given business.
        //Properties: Name, Cost, Quantity
        //Methods: Default and Parameterized constructor,
        //and an overriden ToString method
        [Serializable]
        public class Record
        {
            public Record() { }
            public Record(string name, double cost, int quantity)
            {
                Name = name;
                Cost = cost;
                Quantity = quantity;
            }
            public string Name { get; set; }
            public double Cost { get; set; }
            public int Quantity { get; set; }
            public override string ToString()
            {
                string cost = '$' + Cost.ToString("0.##");
                return Name.PadRight(27) + cost.PadRight(17) + Quantity;
            }
        }

        //Default Constructor
        public ChildForm()
        {
            InitializeComponent();
            Path = "";
        }

        //Parameterized Constructor
        //Purpose: Initialized the records list with data from the
        //given file path and displays the name of the business
        //taken from the name of the file to the form.
        //Parameters: file path as a string
        public ChildForm(string p)
        {
            InitializeComponent();
            Path = p;
            FileStream input = new FileStream(Path, FileMode.OpenOrCreate, FileAccess.Read);
            while (input.Position != input.Length)
            {
                Record record = (Record)reader.Deserialize(input);
                records.Add(record);
                InventoryLB.Items.Add(record.ToString());
            }
            input.Close();
            string formName = "";
            for(int i = 0; i < Path.Length; i++)
            {
                if (Path[i] == '\\')
                {
                    formName = "";
                }
                else if (Path[i] == '.')
                {
                    i = Path.Length;
                }
                else
                    formName += Path[i];
            }
            this.Text = formName + " Inventory";
            InfoLabel.Text = formName + " Inventory Information";
        }

        //Name: Save
        //Purpose: Saves the data in the records list to
        //a serializable file.
        //Parameters: none
        public void Save()
        {
            File.WriteAllText(Path, "");
            // open file with write access 
            FileStream output = new FileStream(Path, FileMode.OpenOrCreate, FileAccess.Write);
            foreach(Record r in records)
            {
                reader.Serialize(output, r);
            }
            output.Close();
            this.Close();
        }

        //Name: Insert
        //Purpose: Inserts a new inventory item into the 
        //records list and displays it on the form.
        //Parameters: name of item as a string, cost as a double
        //and quantity as an integer
        public void Insert(string name, double cost, int quan)
        {
            Record record = new Record(name, cost, quan);
            records.Add(record);
            InventoryLB.Items.Add(record.ToString());
        }

        //Name: Delete
        //Purpose: Deletes the selected item from the
        //records list and removes it from the list box
        //Parameters: none
        public void Delete()
        {
            int index = InventoryLB.SelectedIndex;
            InventoryLB.Items.RemoveAt(index);
            records.Remove(records[index]);
        }

        //Name: UpdateItem
        //Purpose: Updates the current information about an
        //inventory item from the records list and the form's
        //list box
        //Parameters: none
        public void UpdateItem()
        {
            int index = InventoryLB.SelectedIndex;
            string name = records[index].Name;
            double cost = records[index].Cost;
            int quan = records[index].Quantity;
            InputForm inf = new InputForm(name, cost, quan);
            inf.ShowDialog();
            if (inf.Valid)
            {
                records[index].Name = inf.ItemName;
                records[index].Cost = inf.Cost;
                records[index].Quantity = inf.Quantity;
                InventoryLB.Items.Insert(index, records[index]);
                InventoryLB.Items.RemoveAt(index+1);
            }
        }
    }
}
