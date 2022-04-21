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
                     ORDER BY SerialNumber DESC ";
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
                                SerialNumber = (int)reader["SerialNumber"],
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
                     ORDER BY CreateDate ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        command.Parameters.AddWithValue("@QID", questionnaireID);
                        SqlDataReader reader = command.ExecuteReader();

                        List<QuestionModel> questionsList = new List<QuestionModel>();
                        while (reader.Read())
                        {

                            QuestionModel question = new QuestionModel()
                            {
                                QID = (Guid)reader["QID"],
                                QuestionID = (Guid)reader["QuestionID"],
                                //QuestionNumber = (int)reader["QuestionNumber"],
                                Question = reader["Question"] as string,
                                AnswerOption = reader["AnswerOption"] as string,
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
        public SummaryModel GetQuestionnaireSummary(Guid questionnaireID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [QSummarys]
                     WHERE QID = @QID ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        command.Parameters.AddWithValue("@QID", questionnaireID);
                        SqlDataReader reader = command.ExecuteReader();

                        SummaryModel summary = new SummaryModel();
                        if(reader.Read())
                        {
                            summary = new SummaryModel()
                            {
                                QID = (Guid)reader["QID"],
                                Caption = reader["Caption"] as string,
                                Description = reader["Description"] as string,
                                StartDate = (DateTime)reader["StartDate"],
                                EndDate = (DateTime)reader["EndDate"],
                                ViewLimit = (bool)reader["ViewLimit"]
                            };
                        }
                        return summary;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnaireManager.GetQuestionnaireSummary", ex);
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
                        command.Parameters.AddWithValue("@StartDate", qSummary.StartDate.ToString("yyyy/MM/dd"));
                        command.Parameters.AddWithValue("@EndDate", qSummary.EndDate.ToString("yyyy/MM/dd"));
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
        public void CreateQuestion(QuestionModel q)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  INSERT INTO [Questions] 
                        (QID, QuestionID, Question, AnswerOption, QType, IsRequired, CreateDate)
                    VALUES  
                        (@QID, @QuestionID, @Question, @AnswerOption, @QType, @IsRequired, @CreateDate) ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        command.Parameters.AddWithValue("@QuestionID", q.QuestionID);
                        command.Parameters.AddWithValue("@QID", q.QID);
                        command.Parameters.AddWithValue("@Question", q.Question);
                        command.Parameters.AddWithValue("@AnswerOption", q.AnswerOption);
                        command.Parameters.AddWithValue("@QType", q.QType);
                        command.Parameters.AddWithValue("@IsRequired", q.IsRequired);
                        command.Parameters.AddWithValue("@CreateDate", q.CreateDate);

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