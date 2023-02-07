using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace MyMass.Pages.Members
{
    public class CreateModel : PageModel
    {
        public MemberInfo MemberInfo = new MemberInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            MemberInfo.name = Request.Form["name"];
            MemberInfo.email = Request.Form["email"];
            MemberInfo.phone = Request.Form["phone"];
            MemberInfo.address = Request.Form["address"];

            if ( MemberInfo.name.Length == 0 || MemberInfo.email.Length == 0 || MemberInfo.phone.Length == 0 || MemberInfo.address.Length == 0 )
            {
                errorMessage = "All the fields are required";
                return;
            }

            // save the new members into the datadbase

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=MyMass;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO members " +
                                  "(name, email, phone, address) VALUES " +
                                  "(@name, @email, @phone, @address);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", MemberInfo.name);
                        command.Parameters.AddWithValue("@email", MemberInfo.email);
                        command.Parameters.AddWithValue("@phone", MemberInfo.phone);
                        command.Parameters.AddWithValue("@address", MemberInfo.address);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch ( Exception ex )
            {
                errorMessage = ex.Message;
                return;
            }
            MemberInfo.name = ""; MemberInfo.email = ""; MemberInfo.phone = ""; MemberInfo.address = "";
            successMessage = "New member added successfuly";

            Response.Redirect("/Members/Index");
        }
    }
}
