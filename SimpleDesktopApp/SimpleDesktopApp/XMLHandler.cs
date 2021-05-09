using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace SimpleDesktopApp
{
    class XMLHandler
    {
        public static List<User> LoadData()
        {
            // Convert XML data to class User.
            List<User> users = new List<User>();

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<User>));

                var filename = AppDomain.CurrentDomain.BaseDirectory + "UserData.xml";

                using (Stream reader = new FileStream(filename, FileMode.Open))
                {
                    users = (List<User>)serializer.Deserialize(reader);
                    reader.Close();
                }

                return users;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"There was an error that occured while attempting to load the data. \n{ex.Message}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return users;
        }

        public static void SaveData(List<User> users)
        {
            // Convert data from Class User to XML.
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<User>));

                if(users.Count > 0)
                {
                    using (TextWriter writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "UserData.xml"))
                    {
                        serializer.Serialize(writer, users);
                        writer.Close();
                    }
                }
                else
                {
                    MessageBox.Show("There is no data in the table to save.", "No Data!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                MessageBox.Show("Successfully saved data to XML file.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"An error that occured while attempting to save the data. \n{ex.Message}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
