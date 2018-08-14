namespace Database
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    class DatabaseManager
    {
        #region Setup + Helper Functions

        private const string connString = "Server=tcp:gamejam.database.windows.net,1433;Initial Catalog=GameJam;Persist Security Info=False;User ID=GameJam;Password=quantumQ9#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        private SqlConnection TestDbConnection()

        {
            SqlConnection myConn = new SqlConnection(connString);
            try
            {
                myConn.Open();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return myConn;
        }

        #endregion

        #region DML Functions

        // Score struct for storing name and corresponding score
        public struct Score
        {
            public string name;
            public int score;
        }

        /*
         * Inserts a new score into the database
         */
        public void AddNewScore(string name, int score)
        {
            // Acesses the database
            SqlConnection myConn = new SqlConnection(connString);
            myConn.Open();

            // Executes the actual query
            SqlCommand myComm = new SqlCommand("INSERT INTO Scoreboard (Name, Score) VALUES ('" + name + "', '" + score + "')");
            myComm.Connection = myConn;
            myComm.ExecuteNonQuery();

            myConn.Close();
        }

        /*
         * Gets all scores from the database
         */
        public List<Score> GetAllScores()
        {
            // Sets up a list of score objects to be returned
            List<Score> highScores = new List<Score>();

            // Acesses the database
            SqlConnection myConn = TestDbConnection();

            // Prepares the query and connects it to the SQLServer connection
            SqlCommand myComm = new SqlCommand("SELECT * FROM Scoreboard ORDER BY Score ASC");
            myComm.Connection = myConn;

            // Executes the actual query, reads the results into a list of Score objects
            using (var rdr = myComm.ExecuteReader())
            {
                while (rdr.Read())
                {
                    highScores.Add(new Score()
                    {
                        name = rdr.GetString(1),
                        score = rdr.GetInt32(2)
                    }
                    );
                }
                rdr.Close();
            }

            myConn.Close();

            return highScores;
        }

        #endregion
    }
}