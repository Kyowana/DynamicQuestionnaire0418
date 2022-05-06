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
        public List<AnswerSummaryModel> GetAList(Guid qID, int pageSize, int pageIndex, out int totalRows)
        {
            int skip = pageSize * (pageIndex - 1);
            if (skip < 0)
                skip = 0;


            string connStr = ConfigHelper.GetConnectionString();

            string commandText =
                $@"SELECT TOP {pageSize} *
                   FROM AnsSummarys
                   WHERE
                        AnswerID NOT IN
                        (
                            SELECT TOP {skip} AnswerID
                            FROM AnsSummarys
                                WHERE QID = @QID 
                            ORDER BY SubmitDate DESC
                        )
                        AND QID = @QID 
                   ORDER BY SubmitDate DESC";

            string commandCountText =
                @" SELECT COUNT(AnswerID)
                   FROM AnsSummarys ";

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
                        reader.Close();

                        command.CommandText = commandCountText;
                        totalRows = (int)command.ExecuteScalar();
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
        public AnswerContentModel GetAnswerContent(Guid questionID, Guid answerID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [AnsContents]
                     WHERE QuestionID = @QuestionID AND AnswerID = @AnswerID ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@QuestionID", questionID);
                        command.Parameters.AddWithValue("@AnswerID", answerID);
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        AnswerContentModel aContent = new AnswerContentModel();
                        if (reader.Read())
                        {
                            aContent = new AnswerContentModel()
                            {
                                AnswerID = (Guid)reader["AnswerID"],
                                QuestionID = (Guid)reader["QuestionID"],
                                Answer = reader["Answer"] as string,

                            };
                        }
                        return aContent;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("AnswerManager.GetAnswerContent", ex);
                throw;
            }
        }
        public List<AnswerContentModel> GetAnswerListIn1Question(Guid questionID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [AnsContents]
                     WHERE QuestionID = @QuestionID ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@QuestionID", questionID);
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        List<AnswerContentModel> acList = new List<AnswerContentModel>();
                        while (reader.Read())
                        {
                            AnswerContentModel acModel = new AnswerContentModel()
                            {
                                QuestionID = (Guid)reader["QuestionID"],
                                Answer = reader["Answer"] as string,

                            };
                            acList.Add(acModel);
                        }
                        return acList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("AnswerManager.GetAnswerListIn1Question", ex);
                throw;
            }
        }
    }
}