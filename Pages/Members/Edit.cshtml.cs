using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace MyMass.Pages.Members
{
    public class EditInfo : PageModel
    {
        
        public MemberInfo MemberInfo = new MemberInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=MyMass;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM members WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                //MemberInfo memberInfo = new MemberInfo();
                                //memberInfo.id = "" + reader.GetInt32(0);
                                MemberInfo.id = id;
                                MemberInfo.name = reader.GetString(1);
                                MemberInfo.email = reader.GetString(2);
                                MemberInfo.phone = reader.GetString(3);
                                MemberInfo.address = reader.GetString(4);
                                

                               
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost() 
        {
            MemberInfo.id = Request.Form["id"];
            MemberInfo.name = Request.Form["name"];
            MemberInfo.email = Request.Form["email"];
            MemberInfo.phone = Request.Form["phone"];
            MemberInfo.address = Request.Form["address"];

            if (MemberInfo.name.Length == 0 || MemberInfo.email.Length == 0 || MemberInfo.phone.Length == 0 || MemberInfo.address.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=MyMass;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE members " +
                                  "SET name=@name, email=@email, phone=@phone, address=@address " +
                                  "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", MemberInfo.name);
                        command.Parameters.AddWithValue("@email", MemberInfo.email);
                        command.Parameters.AddWithValue("@phone", MemberInfo.phone);
                        command.Parameters.AddWithValue("@address", MemberInfo.address);
                        command.Parameters.AddWithValue("@id", MemberInfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Members/Index");
        }
    }
}
