using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSoft.LawDocsLib
{
    public class PermissionResult
    {
        public bool IsAllowAccess = false;
        public FunctionRoles mFunctionRoles = new FunctionRoles();
        public string Msg = "";
    }

    public class FunctionRoles
    {
        public string FunctionName { get; set; }
        public string FunctionDesc { get; set; }
        public string Roles { get; set; }
        public string MsgNotPermission { get; set; }

        public FunctionRoles()
        {
        }
        public FunctionRoles(string mFunctionName, string mFunctionDesc, string mRoles)
        {
            FunctionName = mFunctionName;
            FunctionDesc = mFunctionDesc;
            Roles = mRoles;
        }

        public static FunctionRoles Static_GetByFunctionName(string mFunctionName, List<FunctionRoles> lFunctionRoles)
        {
            FunctionRoles m_Result = new FunctionRoles();
            m_Result = lFunctionRoles.Find(i => i.FunctionName == mFunctionName);
            if (m_Result == null) m_Result = new FunctionRoles();
            return m_Result;
        }
    }

    public class FunctionList
    {
        public static FunctionRoles XemLuocDoVBPQ
        {
            get
            {
                FunctionRoles m_FunctionRoles = new FunctionRoles("VBPQ_XemLuocDo", "Xem lược đồ VBPQ", "TC,TA,NC");
                m_FunctionRoles.MsgNotPermission = "Bạn chưa có quyền truy cập chức năng này";
                return m_FunctionRoles;
            }
        }
        public static FunctionRoles XemTomtatYChinhVB
        {
            get
            {
                FunctionRoles m_FunctionRoles = new FunctionRoles("VBPQ_XemTomtatYChinhVB", "Xem tóm tắt ý chính văn bản", "TC,NC");
                m_FunctionRoles.MsgNotPermission = "Bạn chưa có quyền truy cập chức năng này";
                return m_FunctionRoles;
            }
        }
        public static FunctionRoles XemVanbanLienquan
        {
            get
            {
                FunctionRoles m_FunctionRoles = new FunctionRoles("VBPQ_XemVanbanLienquan", "Xem văn bản liên quan", "TC,NC");
                m_FunctionRoles.MsgNotPermission = "Bạn chưa có quyền truy cập chức năng này";
                return m_FunctionRoles;
            }
        }

        public static List<FunctionRoles> GetList()
        {
            List<FunctionRoles> m_Result = new List<FunctionRoles>();

            m_Result.Add(XemLuocDoVBPQ);
            m_Result.Add(XemTomtatYChinhVB);
            m_Result.Add(XemVanbanLienquan);

            return m_Result;
        }

    }
    public class RoleHelpers
    {
        public static PermissionResult CheckPermission(List<Roles> lRoles, string mFunctionName)
        {
            PermissionResult m_Result = new PermissionResult();
            List<FunctionRoles> l_FunctionRoles = FunctionList.GetList();
            m_Result.mFunctionRoles = FunctionRoles.Static_GetByFunctionName(mFunctionName, l_FunctionRoles);
            foreach (Roles mRole in lRoles)
            {
                foreach (string mRoleFun in m_Result.mFunctionRoles.Roles.Split(','))
                {
                    if (mRole.RoleName.Trim().ToUpper() == mRoleFun.Trim().ToUpper())
                    {
                        m_Result.IsAllowAccess = true;
                        m_Result.Msg = "OK";
                        break;
                    }
                }
                if (m_Result.IsAllowAccess == true) break;
            }
            if (m_Result.IsAllowAccess == false) m_Result.Msg = "Bạn chưa có quyền xem chức năng này.";

            return m_Result;
        }
    }
}
