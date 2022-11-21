using BOLayer;
using System.Text.Json;

namespace DataLayer
{
    public class Data
    {
        // Storing Data into a text file
        public void AddToFile<T>(T obj)
        {
            string jsonOutput = JsonSerializer.Serialize(obj);
            if(obj is Admin)
            {
                File.AppendAllText("admins.txt", jsonOutput + Environment.NewLine);
            }
            else if(obj is Customer)
            {
                File.AppendAllText("customers.txt", jsonOutput + Environment.NewLine);
            }
            else if (obj is Transaction)
            {
                File.AppendAllText("transactions.txt", jsonOutput + Environment.NewLine);
            }
        }

        // Clears last data & Save new list to file in Json format
        public void SaveToFile<T>(List<T> list)
        {
            string jsonOutput = JsonSerializer.Serialize(list[0]);
            if (list[0] is Admin)
            {
                File.WriteAllText("admin.txt", jsonOutput + Environment.NewLine);
            }
            else if (list[0] is Customer)
            {
                File.WriteAllText("customers.txt", jsonOutput + Environment.NewLine);
            }
            
            // Appends the other objects of list to the file
            for(int i = 1; i < list.Count; i++)
            {
                AddToFile(list[i]);
            }
        }

        // Returns a list of objects from file
        public List<T> ReadFile<T>(string FileName)
        {
            List<T> list = new List<T>();
            string filePath = Path.Combine(Environment.CurrentDirectory, FileName);
            StreamReader sr = new StreamReader(filePath);

            string line = String.Empty;
            while((line = sr.ReadLine()) != null)
            {
                list.Add(JsonSerializer.Deserialize<T>(line));
            }
            sr.Close();

            return list;
        }

        // Deletes a customer object from file
        public void DeleteFromFile(Customer customer)
        {
            List<Customer> list = ReadFile<Customer>("customers.txt");
            // Checking and remove the required object from the list
            foreach(Customer item in list)
            {
                if(item.AccountNo == customer.AccountNo)
                {
                    list.Remove(item);
                    break;
                }
            }
            // Overwriting the list to file
            SaveToFile<Customer>(list);
        }

        // Updates a customer object in the file
        public void UpdateInFile(Customer customer)
        {
            List<Customer> list = ReadFile<Customer>("customers.txt");
        }
    }
}