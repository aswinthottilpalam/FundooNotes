using CommonLayer.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Entities;
using RepositoryLayer.FundooContext;
//using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Experimental.System.Messaging;
using CommonLayer;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        FundoosContext fundoosContext;
        public IConfiguration Configuration { get;  }
        public UserRL(FundoosContext fundoosContext, IConfiguration configuration)
        {
            this.fundoosContext = fundoosContext;
            this.Configuration = configuration;
        }

        // Add User details for registration
        public void AddUser(UserPostModel user)
        {
            try
            {
                User userdata = new User();
                userdata.UserId = user.userId;
                userdata.FirstName = user.firstName;
                userdata.LastName = user.lastName;
                userdata.Email = user.email;
                userdata.Password = EncryptPassword(user.password);
                fundoosContext.Add(userdata);
                fundoosContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Encrypt Password
        private string EncryptPassword(string password)
        {
            try
            {
                if (string.IsNullOrEmpty(password))
                {
                    return null;
                }
                else
                {
                    byte[] b = Encoding.ASCII.GetBytes(password);
                    string encrypted = Convert.ToBase64String(b);
                    return encrypted;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        // Login with the username and password
        public string LoginUser(string email, string password)
        {
            try
            {
                var result = fundoosContext.User.FirstOrDefault(u => u.Email == email && u.Password == password);
                if (result == null)
                {
                    return null;
                }
                return GenerateJWTToken(email, result.UserId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private string GenerateJWTToken(string email, int UserId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("email", email),
                    new Claim("userID",UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),

                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // Forgot password
        public bool ForgotPassword(string email)
        {
            try
            {
                var userdata = fundoosContext.User.FirstOrDefault(u => u.Email == email);
                if(userdata == null)
                {
                    return false;
                }

                MessageQueue queue;
                // Add Message to Queue
                
                if (MessageQueue.Exists(@".\Private$\FundooQueue"))
                {
                    queue = new MessageQueue(@".\Private$\FundooQueue");
                }
                else
                {
                    queue = MessageQueue.Create(@".\Private$\FundooQueue");
                }

                Message MyMessage = new Message();
                MyMessage.Formatter = new BinaryMessageFormatter();
                MyMessage.Body = GenerateJWTToken(email, userdata.UserId);
                MyMessage.Label = "Forgot Password Email";
                queue.Send(MyMessage);

                Message msg = queue.Receive();
                msg.Formatter = new BinaryMessageFormatter();
                EmailService.SendMail(email, msg.Body.ToString());
                queue.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReciveCompleted);

                queue.BeginReceive();
                queue.Close();

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void msmqQueue_ReciveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                {
                    MessageQueue queue = (MessageQueue)sender;
                    Message msg = queue.EndReceive(e.AsyncResult);
                    EmailService.SendMail(e.Message.ToString(), GenerateToken(e.Message.ToString()));
                    queue.BeginReceive();
                }

            }
            catch (MessageQueueException ex)
            {

                if (ex.MessageQueueErrorCode ==
                   MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access is denied. " +
                        "Queue might be a system queue.");
                }
                // Handle other sources of MessageQueueException.
            }
        }

        private string GenerateToken(string email)
        {
            if (email == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Email", email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }



        public bool ChangePassword(string email, ChangePasswordModel valid)
        {
            try
            {
                if (valid.Password.Equals(valid.confirmPassword))
                {
                    var user = fundoosContext.User.Where(u => u.Email == email).FirstOrDefault();
                    user.Password = EncryptPassword(valid.confirmPassword);
                    fundoosContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
