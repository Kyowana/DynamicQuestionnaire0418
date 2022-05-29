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
                        if (reader.Read())
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

        public QuestionModel FindQuestion(Guid questionID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [Questions]
                     WHERE QuestionID = @QuestionID ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        command.Parameters.AddWithValue("@QuestionID", questionID);
                        SqlDataReader reader = command.ExecuteReader();

                        QuestionModel question = new QuestionModel();
                        if (reader.Read())
                        {
                            question = new QuestionModel()
                            {
                                QuestionID = (Guid)reader["QuestionID"],
                                Question = reader["Question"] as string,
                                AnswerOption = reader["AnswerOption"] as string,
                                QType = (int)reader["QType"]
                            };
                            return question;
                        }
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnaireManager.FindQuestion", ex);
                throw;
            }
        }
        //public QuestionModel GetQuestions(Guid qID)
        //{
        //    string connStr = ConfigHelper.GetConnectionString();
        //    string commandText =
        //        $@"  SELECT *
        //             FROM [Questions]
        //             WHERE QID = @QID ";
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connStr))
        //        {
        //            using (SqlCommand command = new SqlCommand(commandText, conn))
        //            {
        //                conn.Open();
        //                command.Parameters.AddWithValue("@QID", qID);
        //                SqlDataReader reader = command.ExecuteReader();

        //                QuestionModel question = new QuestionModel();
        //                while (reader.Read())
        //                {
        //                    question = new QuestionModel()
        //                    {
        //                        QID = (Guid)reader["QID"],
        //                        QuestionID = (Guid)reader["QuestionID"],
        //                        //QuestionNumber = (int)reader["QuestionNumber"],
        //                        Question = reader["Question"] as string,
        //                        AnswerOption = reader["AnswerOption"] as string,
        //                        QType = (int)reader["QType"],
        //                        IsRequired = (bool)reader["IsRequired"],
        //                        CreateDate = (DateTime)reader["CreateDate"]
        //                    };
        //                    return question;
        //                }
        //                return null;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteLog("QuestionnaireManager.GetQuestions", ex);
        //        throw;
        //    }
        //}

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
        public void UpdateSummary(SummaryModel qSummary)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  UPDATE [QSummarys] 
                    SET Caption = @Caption, 
                        Description = @Description,
                        StartDate = @StartDate,
                        EndDate = @EndDate,
                        ViewLimit = @ViewLimit
                    WHERE QID = @QID ";
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
                Logger.WriteLog("QuestionnaireManager.UpdateSummary", ex);
                throw;
            }
        }
        public void DeleteSummary(Guid qID)
        {
            DeleteQuestionOfQuestionnaire(qID);

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  DELETE FROM [QSummarys] 
                    WHERE QID = @QID ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        command.Parameters.AddWithValue("@QID", qID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnaireManager.DeleteSummary", ex);
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
        public void UpdateQuestion(QuestionModel q)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  UPDATE [Questions] 
                    SET Question = @Question, 
                        AnswerOption = @AnswerOption,
                        QType = @QType,
                        IsRequired = @IsRequired
                    WHERE QuestionID = @QuestionID ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        command.Parameters.AddWithValue("@QuestionID", q.QuestionID);
                        command.Parameters.AddWithValue("@Question", q.Question);
                        command.Parameters.AddWithValue("@AnswerOption", q.AnswerOption);
                        command.Parameters.AddWithValue("@QType", q.QType);
                        command.Parameters.AddWithValue("@IsRequired", q.IsRequired);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnaireManager.UpdateQuestion", ex);
                throw;
            }
        }
        public void DeleteQuestionOfQuestionnaire(Guid qID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  DELETE FROM [Questions] 
                    WHERE
                        QID = @QID ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        command.Parameters.AddWithValue("@QID", qID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnaireManager.DeleteQuestionOfQuestionnaire", ex);
                throw;
            }
        }
        public void DeleteQuestion(Guid questionID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  DELETE FROM [Questions] 
                    WHERE
                        QuestionID = @QuestionID ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        command.Parameters.AddWithValue("@QuestionID", questionID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnaireManager.DeleteQuestion", ex);
                throw;
            }
        }

        public List<SummaryModel> GetSearchedList(string keyword)
        {
            string whereCondition = string.Empty;
            if (!string.IsNullOrWhiteSpace(keyword))
                whereCondition = " WHERE Caption LIKE '%'+@keyword+'%' ";

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@" SELECT *
                    FROM QSummarys
                    {whereCondition}
                    ORDER BY SerialNumber DESC ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(keyword))
                        {
                            command.Parameters.AddWithValue("@keyword", keyword);
                        }

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        List<SummaryModel> qList = new List<SummaryModel>();    // 將資料庫內容轉為自定義型別清單
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
                Logger.WriteLog("QuestionnaireManager.GetSearchedList", ex);
                throw;
            }
        }
        public List<SummaryModel> GetQList(string keyword, string startDate, string endDate, int pageSize, int pageIndex, out int totalRows)
        {
            int skip = pageSize * (pageIndex - 1);
            if (skip < 0)
                skip = 0;

            string whereCondition = string.Empty;
            string whereCondition1 = string.Empty;
            //if (!string.IsNullOrWhiteSpace(keyword))
            //{
            //    whereCondition = "WHERE Caption LIKE '%'+@keyword+'%'";
            //    whereCondition1 = "AND Caption LIKE '%'+@keyword+'%'";
            //}

            if (string.IsNullOrWhiteSpace(keyword))
            {
                if (string.IsNullOrWhiteSpace(startDate))
                {
                    if (string.IsNullOrWhiteSpace(endDate))
                    {
                        whereCondition = string.Empty;
                        whereCondition1 = string.Empty;
                    }
                    else
                    {
                        whereCondition = "WHERE EndDate <= @end ";
                        whereCondition1 = "AND EndDate <= @end ";
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(endDate))
                    {
                        whereCondition = "WHERE StartDate >= @start ";
                        whereCondition1 = "AND StartDate >= @start ";
                    }
                    else
                    {
                        whereCondition = "WHERE StartDate >= @start AND EndDate <= @end ";
                        whereCondition1 = "AND StartDate >= @start AND EndDate <= @end ";
                    }
                }

            }
            else
            {
                if (string.IsNullOrWhiteSpace(startDate))
                {
                    if (string.IsNullOrWhiteSpace(endDate))
                    {
                        whereCondition = "WHERE Caption LIKE '%'+@keyword+'%'";
                        whereCondition1 = "AND Caption LIKE '%'+@keyword+'%'";
                    }
                    else
                    {
                        whereCondition = "WHERE Caption LIKE '%'+@keyword+'%' AND EndDate <= @end ";
                        whereCondition1 = "AND Caption LIKE '%'+@keyword+'%' AND EndDate <= @end ";
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(endDate))
                    {
                        whereCondition = "WHERE Caption LIKE '%'+@keyword+'%' AND StartDate >= @start ";
                        whereCondition1 = "AND Caption LIKE '%'+@keyword+'%' AND StartDate >= @start ";
                    }
                    else
                    {
                        whereCondition = "WHERE Caption LIKE '%'+@keyword+'%' AND StartDate >= @start AND EndDate <= @end ";
                        whereCondition1 = "AND Caption LIKE '%'+@keyword+'%' AND StartDate >= @start AND EndDate <= @end ";
                    }
                }
            }

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"SELECT TOP {pageSize} *
                   FROM QSummarys
                   WHERE
                        QID NOT IN
                        (
                            SELECT TOP {skip} QID
                            FROM QSummarys
                                {whereCondition}
                            ORDER BY SerialNumber DESC
                        )
                        {whereCondition1}
                   ORDER BY SerialNumber DESC";

            string commandCountText =
                $@" SELECT COUNT(QID)
                   FROM QSummarys
                   {whereCondition}" ;

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(keyword))
                            command.Parameters.AddWithValue("@keyword", keyword);
                        if (!string.IsNullOrWhiteSpace(startDate))
                            command.Parameters.AddWithValue("@start", startDate.Replace("-", "/"));
                        if (!string.IsNullOrWhiteSpace(endDate))
                            command.Parameters.AddWithValue("@end", endDate.Replace("-", "/"));

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        List<SummaryModel> retList = new List<SummaryModel>();
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
                            retList.Add(qSummary);
                        }
                        reader.Close();

                        command.CommandText = commandCountText;
                        totalRows = (int)command.ExecuteScalar();
                        if (!string.IsNullOrWhiteSpace(keyword) || !string.IsNullOrWhiteSpace(startDate) || !string.IsNullOrWhiteSpace(endDate))
                        {
                            command.Parameters.Clear();
                            if (!string.IsNullOrWhiteSpace(keyword))
                                command.Parameters.AddWithValue("@keyword", keyword);
                            if (!string.IsNullOrWhiteSpace(startDate))
                                command.Parameters.AddWithValue("@start", startDate.Replace("-", "/"));
                            if (!string.IsNullOrWhiteSpace(endDate))
                                command.Parameters.AddWithValue("@end", endDate.Replace("-", "/"));
                            totalRows = (int)command.ExecuteScalar();
                        }

                        return retList;
                    }
                }
            }
            catch (Exception ex)
            {
                // 丟出前先記錄
                Logger.WriteLog("QuestionnaireManager.GetQList", ex); // 使用類別名稱+方法名稱
                throw;
            }
        }
    }
}