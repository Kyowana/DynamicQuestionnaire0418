using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using 動態問卷.Helpers;
using 動態問卷.Models;

namespace 動態問卷.Managers
{
    public class AnswerManager
    {
        public void CreateAnswerSummary(AnswerSummaryModel aSummary)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  INSERT INTO [AnsSummarys] 
                        (AnswerID, QID, Name, Phone, Email, Age, SubmitDate)
                    VALUES  
                        (@AnswerID, @QID, @Name, @Phone, @Email, @Age, @SubmitDate) ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();

                        command.Parameters.AddWithValue("@AnswerID", aSummary.AnswerID);
                        command.Parameters.AddWithValue("@QID", aSummary.QID);
                        command.Parameters.AddWithValue("@Name", aSummary.Name);
                        command.Parameters.AddWithValue("@Phone", aSummary.Phone);
                        command.Parameters.AddWithValue("@Email", aSummary.Email);
                        command.Parameters.AddWithValue("@Age", aSummary.Age);
                        command.Parameters.AddWithValue("@SubmitDate", DateTime.Now);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("AnswerManager.CreateAnswerSummary", ex);
                throw;
            }
        }
        public void CreateAnswerContent(AnswerContentModel aContent)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  INSERT INTO [AnsContents] 
                        (ID, AnswerID, QuestionID, Answer)
                    VALUES  
                        (@ID, @AnswerID, @QuestionID, @Answer) ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();

                        command.Parameters.AddWithValue("@ID", Guid.NewGuid());
                        command.Parameters.AddWithValue("@AnswerID", aContent.AnswerID);
                        command.Parameters.AddWithValue("@QuestionID", aContent.QuestionID);
                        command.Parameters.AddWithValue("@Answer", aContent.Answer);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("AnswerManager.CreateAnswerContent", ex);
                throw;
            }
        }

        public List<AnswerSummaryModel> GetAList(Guid qID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [AnsSummarys]
                     WHERE QID = @QID 
                     ORDER BY SubmitDate DESC ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@QID", qID);
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        List<AnswerSummaryModel> asList = new List<AnswerSummaryModel>();
                        while (reader.Read())
                        {
                            AnswerSummaryModel aSummary = new AnswerSummaryModel()
                            {
                                AnswerID = (Guid)reader["AnswerID"],
                                QID = (Guid)reader["QID"],
                                //SerialNumber = (Guid)reader["SerialNumber"],
                                Name = reader["Name"] as string,
                                Phone = reader["Phone"] as string,
                                Email = reader["Email"] as string,
                                Age = (int)reader["Age"],
                                SubmitDate = (DateTime)reader["SubmitDate"]
                            };
                            asList.Add(aSummary);
                        }
                        return asList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("AnswerManager.GetAList", ex);
                throw;
            }
        }
    }
}