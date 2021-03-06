﻿using Database;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Webservice.Controllers
{
    public class UserController : BaseController
    {
        UserRepository userRepository = new UserRepository(provider);

        [HttpGet]
        [ActionName("getUser")]
        public IHttpActionResult getUser(string id)
        {
            using(IDbConnection connection = provider.getConnection())
            {
                User user;
                try
                {
                    user = userRepository.getUser(connection, id);
                }
                catch
                {
                    return BadRequest();
                }
                
                if(user == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(user);
                }
            }
        }

        [HttpGet]
        [ActionName("getUsers")]
        public IHttpActionResult getUsers()
        {
            using (IDbConnection connection = provider.getConnection())
            {
                List<User> users;
                try
                {
                    users = userRepository.getUsers(connection);
                }
                catch
                {
                    return BadRequest();
                }

                if (users == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(users);
                }
            }
        }

        [HttpGet]
        [ActionName("getContacts")]
        public IHttpActionResult getContacts(string id)
        {
            using (IDbConnection connection = provider.getConnection())
            {
                List<User> users;
                try
                {
                    users = userRepository.getContacts(connection, id);
                }
                catch (Exception e)
                {
                    return BadRequest(e.ToString());
                }
                return Ok(users);
            }
        }

        [HttpPost]
        [ActionName("register")]
        public IHttpActionResult register(string username, string password, string firstName, string lastName)
        {
            using (IDbConnection connection = provider.getConnection())
            {
                User user;
                try
                {
                    user = userRepository.createUser(connection, username, password, firstName, lastName);
                }
                catch (Exception e)
                {
                    return BadRequest(e.ToString());
                }
                 return Ok(user);
            }
        }

        [HttpPost]
        [ActionName("login")]
        public IHttpActionResult login([FromBody] User user)
        {
            using (IDbConnection connection = provider.getConnection())
            {
                User loggedInUser;
                try
                {
                    loggedInUser = userRepository.login(connection, user.username, user.password);
                }
                catch (Exception e)
                {
                    return BadRequest(e.ToString());
                }

                if (loggedInUser == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(loggedInUser);
                }
            }
        }

        [HttpPost]
        [ActionName("logout")]
        public IHttpActionResult logout(string username)
        {
            using (IDbConnection connection = provider.getConnection())
            {
                User loggedOutUser;
                try
                {
                    loggedOutUser = userRepository.logout(connection, username);
                }
                catch
                {
                    return BadRequest();
                }

                if (loggedOutUser == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(loggedOutUser);
                }
            }
        }
    }
}