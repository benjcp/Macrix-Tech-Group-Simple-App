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

                List<User> lstUsers;

                var filename = AppDomain.CurrentDomain.BaseDirectory + "UserData.xml";

                using (Stream reader = new FileStream(filename, FileMode.Open))
                {
                    lstUsers = (List<User>)serializer.Deserialize(reader);
                }

                return lstUsers;
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error that occured while attempting to load the data.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }

                return users;
        }

        public static void SaveData(List<User> users)
        {
            // Convert data from Class User to XML.
            try
            {
                System.Xml.Serialization.XmlSerializer writer =
                    new System.Xml.Serialization.XmlSerializer(typeof(List<User>));

                var path = AppDomain.CurrentDomain.BaseDirectory + "UserData.xml";
                FileStream file = File.Create(path);

                writer.Serialize(file, users);
            }
            catch(Exception ex)
            {
                MessageBox.Show("There was an error that occured while attempting to save the data.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }
    }
}
