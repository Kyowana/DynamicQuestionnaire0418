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
        public List<QuestionModel> GetQuestionsList(Guid questionnaireID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [Questions]
                     WHERE QID = @QID
                     ORDER BY CreateTime ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        List<QuestionModel> questionsList = new List<QuestionModel>();
                        while (reader.Read())
                        {
                            QuestionModel question = new QuestionModel()
                            {
                                QID = (Guid)reader["QID"],
                                QuestionID = (Guid)reader["QuestionID"],
                                Question = reader["Question"] as string,
                                QType = (int)reader["QType"],
                                IsRequired = (bool)reader["IsRequired"],
                                CreateDate = (DateTime)reader["CreateDate"]
                            };
                            questionsList.Add(question);
                        }
                        return questionsList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnaireManager.GetQuestionsList", ex);
                throw;
            }
        }

        public void CreateQuestionnaire(SummaryModel qSummary)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  INSERT INTO [QSummarys] 
                        (QID, Caption, Description, StartDate, EndDate, ViewLimit)
                    VALUES  
                        (@QID, @Caption, @Description, @StartDate, @EndDate, @ViewLimit) ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();

                            command.Parameters.AddWithValue("@QID", qSummary.QID);
                            command.Parameters.AddWithValue("@Caption", qSummary.Caption);
                            command.Parameters.AddWithValue("@Description", qSummary.Description);
                            command.Parameters.AddWithValue("@StartDate", qSummary.StartDate);
                            command.Parameters.AddWithValue("@EndDate", qSummary.EndDate);
                            command.Parameters.AddWithValue("@ViewLimit", qSummary.ViewLimit);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnaireManager.CreateQuestionnaire", ex);
                throw;
            }
        }
        public void CreateQuestion(List<QuestionModel> qList)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  INSERT INTO [Questions] 
                        (QID, Question, QType, IsRequired, CreateDate)
                    VALUES  
                        (@QID, @Question, @QType, @IsRequired, @CreateDate) ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();

                        foreach (var item in qList)
                        {
                            command.Parameters.AddWithValue("@QID", item.QID);
                            command.Parameters.AddWithValue("@Question", item.Question);
                            command.Parameters.AddWithValue("@QType", item.QType);
                            command.Parameters.AddWithValue("@IsRequired", item.IsRequired);
                            command.Parameters.AddWithValue("@CreateDate", item.CreateDate);

                        }

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