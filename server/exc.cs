private void button1_Click(object sender, EventArgs e)
{
    string sqlFilePath = "C:\\path\\to\\your\\mysql.sql"; // Replace with the actual path to your .sql file

    try
    {
        string sqlScript = File.ReadAllText(sqlFilePath);
        MessageBox.Show("SQL script read successfully!"); // Optional: Confirm the file was read

    }
    catch (Exception ex)
    {
        MessageBox.Show("Error reading SQL script: " + ex.Message);
    }
}
