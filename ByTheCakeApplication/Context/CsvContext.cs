namespace ByTheCakeApplication.Context
{
    using Contracts;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Models;

    public class CsvContext : IContext
    {
        private readonly string contextPath = Directory.GetCurrentDirectory() + "/Context/";
        private readonly string contextFileName = "database.csv";

        /// <summary>
        /// Initializes database file if the file is not existent yet.
        /// </summary>
        /// <param name="deleteDatabaseIfExistent">Should the database if existent be created anew or not.</param>
        public void InitializeDb(bool deleteDatabaseIfExistent)
        {
            DirectoryInfo info = new DirectoryInfo(this.contextPath);

            FileInfo[] files = info.GetFiles();

            if (deleteDatabaseIfExistent || !files.Any(f => f.Name.Contains("database") && f.Extension.Contains("csv")))
            {
                FileStream file = File.Create($"{this.contextPath}/database.csv");
                file.Dispose();
            }
        }

        public string Add(Cake cake)
        {
            if (cake == null)
            {
                throw new ArgumentException("Cake cannot be null!");
            }

            using (FileStream fs = new FileStream(this.contextPath + "/" + this.contextFileName, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    string currentCake = reader.ReadLine();
                    bool cakeExists = false;

                    while (true)
                    {
                        if (string.IsNullOrEmpty(currentCake))
                        {
                            break;
                        }

                        string[] currentCakeTokens = currentCake.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(c => c.Trim()).ToArray();

                        if (currentCakeTokens[0] == cake.Name)
                        {
                            cakeExists = true;
                            break;
                        }

                        currentCake = reader.ReadLine();
                    }

                    if (!cakeExists)
                    {
                        using (StreamWriter writer = new StreamWriter(fs))
                        {
                            writer.WriteLine($"{cake.Name}, {cake.Price}");
                        }

                        return $"Cake {cake.Name} with price {cake.Price} has been created!";
                    }
                    else
                    {
                        return $"Cake {cake.Name} already exist!";
                    }
                }
            }
        }

        public ICollection<Cake> GetAll()
        {
            ICollection<Cake> cakes = new List<Cake>();

            using (FileStream fs = new FileStream(this.contextPath + "/" + this.contextFileName, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    string currentCake = reader.ReadLine();

                    while (true)
                    {
                        if (string.IsNullOrEmpty(currentCake))
                        {
                            break;
                        }

                        string[] currentCakeTokens = currentCake.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(c => c.Trim()).ToArray();

                        Cake curretCake = new Cake(currentCakeTokens[0], decimal.Parse(currentCakeTokens[1]));

                        cakes.Add(curretCake);

                        currentCake = reader.ReadLine();
                    }
                }
            }

            return cakes;
        }
    }
}
