using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyMass.Pages.Members
{
    public class IndexModel : PageModel
    {
        public List<MemberInfo> listMembers = new List<MemberInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=MyMass;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM members";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MemberInfo memberInfo = new MemberInfo();
                                memberInfo.id = "" + reader.GetInt32(0);
                                memberInfo.name = reader.GetString(1);
                                memberInfo.email = reader.GetString(2);
                                memberInfo.phone = reader.GetString(3);
                                memberInfo.address = reader.GetString(4);
                                memberInfo.created_at = reader.GetDateTime(5).ToString();

                                listMembers.Add(memberInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }


    public class MemberInfo
    {
        public string id;
        public string name;
        public string email;
        public string phone;
        public string address;
        public string created_at;
            
    }
}

