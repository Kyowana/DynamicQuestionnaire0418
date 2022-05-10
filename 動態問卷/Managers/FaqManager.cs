using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using 動態問卷.Helpers;
using 動態問卷.Models;

namespace 動態問卷.Managers
{
    public class FaqManager
    {
        public List<FaqModel> GetFaqList()
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [Faqs] ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        List<FaqModel> faqList = new List<FaqModel>();
                        while (reader.Read())
                        {
                            FaqModel faq = new FaqModel()
                            {
                                FaqID = (Guid)reader["FaqID"],
                                QuestionNumber = (int)reader["QuestionNumber"],
                                Question = reader["Question"] as string,
                                AnswerOption = reader["AnswerOption"] as string,
                                QType = (int)reader["QType"],
                                IsRequired = (bool)reader["IsRequired"]
                            };
                            faqList.Add(faq);
                        }
                        return faqList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("FaqManager.GetFaqList", ex);
                throw;
            }
        }
        public void CreateFaq(FaqModel faq)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  INSERT INTO [Faqs] 
                        (FaqID, Question, AnswerOption, QType, IsRequired)
                    VALUES  
                        (@FaqID, @Question, @AnswerOption, @QType, @IsRequired) ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();

                        command.Parameters.AddWithValue("@FaqID", Guid.NewGuid());
                        command.Parameters.AddWithValue("@Question", faq.Question);
                        command.Parameters.AddWithValue("@AnswerOption", faq.AnswerOption);
                        command.Parameters.AddWithValue("@QType", faq.QType);
                        command.Parameters.AddWithValue("@IsRequired", faq.IsRequired);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("FaqManager.CreateFaq", ex);
                throw;
            }
        }
        public void UpdateFaq(FaqModel faq)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  UPDATE [Faqs] 
                    SET Question = @Question,
                        AnswerOption = @AnswerOption,
                        QType = @QType,
                        IsRequired = @IsRequired
                    WHERE FaqID = @FaqID ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        command.Parameters.AddWithValue("@FaqID", faq.FaqID);
                        command.Parameters.AddWithValue("@Question", faq.Question);
                        command.Parameters.AddWithValue("@AnswerOption", faq.AnswerOption);
                        command.Parameters.AddWithValue("@QType", faq.QType);
                        command.Parameters.AddWithValue("@IsRequired", faq.IsRequired);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("FaqManager.UpdateFaq", ex);
                throw;
            }
        }
        public void DeleteFaq(Guid faqID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  DELETE FROM [Faqs] 
                    WHERE FaqID = @FaqID ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        command.Parameters.AddWithValue("@FaqID", faqID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("FaqManager.DeleteFaq", ex);
                throw;
            }
        }
    }
}