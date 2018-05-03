using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Personify.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumFramework.Generators.GeneratorData;
using SeleniumFramework.PageClasses;
using System.Text.RegularExpressions;
using Castle.Core.Internal;
using SimpleLogger;

namespace SeleniumFramework.Helpers
{
    public class MyHelper
    {
        //Note: This class has all general global queries to find basic/existing data set from database, providing this data to test scripts to work with existing data like customer, committee, order, meetings, address etc. in some test scenarios we need to get randam record/records from large tables, for this purpose we used '0.01 >= CAST(CHECKSUM(NEWID(), C.MASTER_CUSTOMER_ID) & 0x7fffffff AS float) / CAST(0x7fffffff AS int)'. please refer[https://msdn.microsoft.com/en-us/library/cc441928.aspx] , using NEWID() as well if table has short dataset
        
        //Find Individual record type with Primary Address
        public static bool GetRandomValidIndividualConstituentIdFromDb(out string constituentId)
        {
        
            var ds = SqlHelper.GetData("Select Top(1) C.MASTER_CUSTOMER_ID As 'Constituent Id' "+
                                        "From CUSTOMER C WITH(NOLOCK) "+
                                        "INNER JOIN CUS_ADDRESS AS CA WITH(NOLOCK) "+
                                        "ON C.MASTER_CUSTOMER_ID = CA.MASTER_CUSTOMER_ID AND C.SUB_CUSTOMER_ID = CA.SUB_CUSTOMER_ID "+
                                        "INNER JOIN CUS_ADDRESS_DETAIL AS CAD WITH(NOLOCK) "+
                                        "ON CA.CUS_ADDRESS_ID = CAD.CUS_ADDRESS_ID "+
                                        "INNER JOIN CUS_SEGMENT_MEMBER AS CSM WITH(NOLOCK) "+
                                        "ON C.MASTER_CUSTOMER_ID = CSM.MASTER_CUSTOMER_ID AND C.SUB_CUSTOMER_ID = CSM.SUB_CUSTOMER_ID "+
                                        $"Where C.MASTER_CUSTOMER_ID like('0%') AND(C.RECORD_TYPE not in ('T', 'S', 'C') AND CSM.SEGMENT_QUALIFIER1 = '{Config.OrgId}' AND CSM.SEGMENT_QUALIFIER2 = '{Config.OrgId}' AND CSM.SEGMENT_RULE_CODE = 'ORG_UNIT') "+
                                        " And 0.01 >= CAST(CHECKSUM(NEWID(), C.MASTER_CUSTOMER_ID) & 0x7fffffff AS float)"+
                                        " / CAST(0x7fffffff AS int)", Config.ConnectionString);

            if (ds.Tables[0].Rows.Count == 1)
            {
                constituentId = ds.Tables[0].Rows[0]["Constituent Id"].ToString();
                return true;
            }
            else
            {
                Assert.Fail(
                    $"There was no data in the DataSet - GetRandomValidIndividualConstituentId(), creating new Customer - {Config.ConnectionString} - {Config.OrgId} ");
                constituentId = null;
                return false;
            }
        }

       
        public static void GetDefaultOrganisationDetails(out string orgId, out string orgUnitId)
        {
            DataSet ds = SqlHelper.GetData("SELECT ORG_ID, ORG_UNIT_ID FROM PSM_ORGUNIT_MEMBER WITH(NOLOCK) WHERE USER_ID='" + Config.UserName + "' AND DEFAULT_FLAG='Y'", Config.ConnectionString);
            orgId = ds.Tables[0].Rows[0]["ORG_ID"].ToString();
            orgUnitId = ds.Tables[0].Rows[0]["ORG_UNIT_ID"].ToString();
       
        }



    }//end of class -PersonifyHelper.cs

    }//end of namespace
