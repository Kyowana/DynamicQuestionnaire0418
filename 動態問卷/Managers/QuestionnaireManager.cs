using 動態問卷.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using 動態問卷.Helpers;
using System.Data.SqlClient;

namespace 動態問卷.Managers
{
    public class QuestionnaireManager
    {
        public List<SummaryModel> GetQList()
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [QSummarys]
                     ORDER BY CreateTime DESC";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        List<SummaryModel> qList = new List<SummaryModel>();
                        while (reader.Read())
                        {
                            SummaryModel qSummary = new SummaryModel()
                            {
                                QID = (Guid)reader["QID"],
                                ViewLimit = (bool)reader["ViewLimit"],
                                Caption = reader["Caption"] as string,
                                Description = reader["Description"] as string,
                                StartDate = (DateTime)reader["StartDate"],
                                EndDate = (DateTime)reader["EndDate"]
                            };
                            qList.Add(qSummary);
                        }
                        return qList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnaireManager.GetQList", ex);
                throw;
            }
        }
        public void CreateQuestion(QuestionModel q)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  INSERT INTO [Questions] 
                        (QID, Question, QType, IsRequired)
                    VALUES  
                        (@QID, @Question, @QType, @IsRequired) ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        command.Parameters.AddWithValue("@QID", q.QID);
                        command.Parameters.AddWithValue("@Question", q.Question);
                        command.Parameters.AddWithValue("@QType", q.QType);
                        command.Parameters.AddWithValue("@IsRequired", q.IsRequired);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnaireManager.CreateQuestion", ex);
                throw;
            }
        }
    }
}