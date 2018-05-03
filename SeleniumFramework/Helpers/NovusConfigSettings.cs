using System;
using System.Collections.Generic;
using Personify.Helpers;

namespace SeleniumFramework.Helpers
{
    public static class NovusConfigSettings
    {
        private static readonly Dictionary<PersonifyFlags, bool> CompanyAttnLine;
        private static readonly Dictionary<PersonifyFlags, bool> CompanyJobTitle;
        private static readonly Dictionary<PersonifyFlags, bool> CompanyLabelName;
        private static readonly Dictionary<PersonifyFlags, bool> CompanyMailStop;
        private static readonly Dictionary<PersonifyFlags, bool> CompanyPersonalLine;
        private static readonly Dictionary<PersonifyFlags, bool> IndividualAttnLine;
        private static readonly Dictionary<PersonifyFlags, bool> IndividualCompanyName;
        private static readonly Dictionary<PersonifyFlags, bool> IndividualJobTitle;
        private static readonly Dictionary<PersonifyFlags, bool> IndividualLabelName;
        private static readonly Dictionary<PersonifyFlags, bool> IndividualMailStop;
        private static readonly Dictionary<PersonifyFlags, bool> IndividualPersonalLine;


        static NovusConfigSettings()
        {
            var companyDs = SqlHelper.GetData("SELECT FIELD_NAME,SCREEN_FLAG,REQUIRED_FLAG FROM CUS_ADDRESS_DETAIL_STRUCTURE WHERE RECORD_TYPE LIKE 'C'", Config.ConnectionString);


            CompanyAttnLine = new Dictionary<PersonifyFlags, bool>();
            CompanyAttnLine.Add(PersonifyFlags.RequiredFlag, StringToBool(companyDs.Tables[0].Rows[4]["REQUIRED_FLAG"].ToString()));
            CompanyAttnLine.Add(PersonifyFlags.ScreenFlag, StringToBool(companyDs.Tables[0].Rows[4]["SCREEN_FLAG"].ToString()));

            CompanyJobTitle = new Dictionary<PersonifyFlags, bool>();
            CompanyJobTitle.Add(PersonifyFlags.RequiredFlag, StringToBool(companyDs.Tables[0].Rows[4]["REQUIRED_FLAG"].ToString()));
            CompanyJobTitle.Add(PersonifyFlags.ScreenFlag, StringToBool(companyDs.Tables[0].Rows[4]["SCREEN_FLAG"].ToString()));

            CompanyLabelName = new Dictionary<PersonifyFlags, bool>();
            CompanyLabelName.Add(PersonifyFlags.RequiredFlag, StringToBool(companyDs.Tables[0].Rows[4]["REQUIRED_FLAG"].ToString()));
            CompanyLabelName.Add(PersonifyFlags.ScreenFlag, StringToBool(companyDs.Tables[0].Rows[4]["SCREEN_FLAG"].ToString()));

            CompanyMailStop = new Dictionary<PersonifyFlags, bool>();
            CompanyMailStop.Add(PersonifyFlags.RequiredFlag, StringToBool(companyDs.Tables[0].Rows[4]["REQUIRED_FLAG"].ToString()));
            CompanyMailStop.Add(PersonifyFlags.ScreenFlag, StringToBool(companyDs.Tables[0].Rows[4]["SCREEN_FLAG"].ToString()));

            CompanyPersonalLine = new Dictionary<PersonifyFlags, bool>();
            CompanyPersonalLine.Add(PersonifyFlags.RequiredFlag, StringToBool(companyDs.Tables[0].Rows[4]["REQUIRED_FLAG"].ToString()));
            CompanyPersonalLine.Add(PersonifyFlags.ScreenFlag, StringToBool(companyDs.Tables[0].Rows[4]["SCREEN_FLAG"].ToString()));

            var indivDs = SqlHelper.GetData("SELECT FIELD_NAME,SCREEN_FLAG,REQUIRED_FLAG FROM CUS_ADDRESS_DETAIL_STRUCTURE WHERE RECORD_TYPE LIKE 'I'", Config.ConnectionString);

            IndividualAttnLine = new Dictionary<PersonifyFlags, bool>();
            IndividualAttnLine.Add(PersonifyFlags.RequiredFlag, StringToBool(indivDs.Tables[0].Rows[4]["REQUIRED_FLAG"].ToString()));
            IndividualAttnLine.Add(PersonifyFlags.ScreenFlag, StringToBool(indivDs.Tables[0].Rows[4]["SCREEN_FLAG"].ToString()));

            IndividualCompanyName = new Dictionary<PersonifyFlags, bool>();
            IndividualCompanyName.Add(PersonifyFlags.RequiredFlag, StringToBool(indivDs.Tables[0].Rows[4]["REQUIRED_FLAG"].ToString()));
            IndividualCompanyName.Add(PersonifyFlags.ScreenFlag, StringToBool(indivDs.Tables[0].Rows[4]["SCREEN_FLAG"].ToString()));

            IndividualJobTitle = new Dictionary<PersonifyFlags, bool>();
            IndividualJobTitle.Add(PersonifyFlags.RequiredFlag, StringToBool(indivDs.Tables[0].Rows[4]["REQUIRED_FLAG"].ToString()));
            IndividualJobTitle.Add(PersonifyFlags.ScreenFlag, StringToBool(indivDs.Tables[0].Rows[4]["SCREEN_FLAG"].ToString()));

            IndividualLabelName = new Dictionary<PersonifyFlags, bool>();
            IndividualLabelName.Add(PersonifyFlags.RequiredFlag, StringToBool(indivDs.Tables[0].Rows[4]["REQUIRED_FLAG"].ToString()));
            IndividualLabelName.Add(PersonifyFlags.ScreenFlag, StringToBool(indivDs.Tables[0].Rows[4]["SCREEN_FLAG"].ToString()));

            IndividualMailStop = new Dictionary<PersonifyFlags, bool>();
            IndividualMailStop.Add(PersonifyFlags.RequiredFlag, StringToBool(indivDs.Tables[0].Rows[4]["REQUIRED_FLAG"].ToString()));
            IndividualMailStop.Add(PersonifyFlags.ScreenFlag, StringToBool(indivDs.Tables[0].Rows[4]["SCREEN_FLAG"].ToString()));

            IndividualPersonalLine = new Dictionary<PersonifyFlags, bool>();
            IndividualPersonalLine.Add(PersonifyFlags.RequiredFlag, StringToBool(indivDs.Tables[0].Rows[4]["REQUIRED_FLAG"].ToString()));
            IndividualPersonalLine.Add(PersonifyFlags.ScreenFlag, StringToBool(indivDs.Tables[0].Rows[4]["SCREEN_FLAG"].ToString()));
            
        }

        private static bool StringToBool(string s)
        {
            if (s == "N")
            {
                return false;
            }
            if (s == "Y")
            {
                return true;
            }
            throw new Exception($"Bad flag value read from database expected Y or N. <\"{s}\">");
        }

        public static bool GetSetting(PersonifyConstituentSettings pSetting,PersonifyFlags pFlag, ConstituentType cType)
        {
            bool value;
            switch (pSetting)
            {
                
                case PersonifyConstituentSettings.AttentionLine:
                    if (cType == ConstituentType.Individual)
                    {
                        IndividualAttnLine.TryGetValue(pFlag, out value);
                        return value;
                    }
                    else
                    {
                        CompanyAttnLine.TryGetValue(pFlag, out value);
                        return value;
                    }
                    
                case PersonifyConstituentSettings.JobTitle:
                    if (cType == ConstituentType.Individual)
                    {
                        IndividualJobTitle.TryGetValue(pFlag, out value);
                        return value;
                    }
                    else
                    {
                        CompanyJobTitle.TryGetValue(pFlag, out value);
                        return value;
                    }

                case PersonifyConstituentSettings.LabelName:
                    if (cType == ConstituentType.Individual)
                    {
                        IndividualLabelName.TryGetValue(pFlag, out value);
                        return value;
                    }
                    else
                    {
                        CompanyLabelName.TryGetValue(pFlag, out value);
                        return value;
                    }

                case PersonifyConstituentSettings.MailStop:
                    if (cType == ConstituentType.Individual)
                    {
                        IndividualMailStop.TryGetValue(pFlag, out value);
                        return value;
                    }
                    else
                    {
                        CompanyMailStop.TryGetValue(pFlag, out value);
                        return value;
                    }

                case PersonifyConstituentSettings.PersonalLine:
                    if (cType == ConstituentType.Individual)
                    {
                        IndividualPersonalLine.TryGetValue(pFlag, out value);
                        return value;
                    }
                    else
                    {
                        CompanyPersonalLine.TryGetValue(pFlag, out value);
                        return value;
                    }

                case PersonifyConstituentSettings.CompanyName:
                    IndividualCompanyName.TryGetValue(pFlag, out value);
                    return value;
                    
                default:
                    throw new Exception("Unexted Setting provided");
                   
            }
            
        }
    }
    
    public enum PersonifyFlags
    {
        RequiredFlag,
        ScreenFlag
    }

    public enum ConstituentType
    {
        Company,
        Individual
    }
    public enum PersonifyConstituentSettings
    {
        AttentionLine,
        JobTitle,
        LabelName,
        MailStop,
        PersonalLine,
        CompanyName
    }
}