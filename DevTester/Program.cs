using DataAccess.DAOs;

public class Program{
    public static void Main(string[] args)
    {
        var sqlOperation = new SqlOperation();
        sqlOperation.ProcedureName = "CRE_USER_PR";

        sqlOperation.AddStringParameter("P_UserCode", "chuangr");
        sqlOperation.AddStringParameter("P_Name", "Chris");
        sqlOperation.AddStringParameter("P_Email", "chuangr@ucenfotec.ac.cr");
        sqlOperation.AddStringParameter("P_Password", "test");
        sqlOperation.AddStringParameter("P_Status", "AC");
        sqlOperation.AddDateTimeParam("P_BirthDate", DateTime.Now);

        var sqlDao = SqlDao.GetInstance();

        sqlDao.ExecuteProcedure(sqlOperation);
    }
}