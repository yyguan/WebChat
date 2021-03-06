﻿using Entity.v1.models;
using System;
using System.Collections.Generic;
using System.Text;
using WebChat.DataAccess;

namespace DataAccess
{
    public class UnitOfWork:IDisposable
    {
        private ChartContext context = new ChartContext();

        private UserRepository userRepository;
        public UserRepository UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new UserRepository(context);
                }
                return userRepository;
            }
        }
        private UserLoginRepository userLoginRepository;
        public UserLoginRepository UserLoginRepository
        {
            get
            {
                if (this.userLoginRepository == null)
                {
                    this.userLoginRepository = new UserLoginRepository(context);
                }
                return userLoginRepository;
            }
        }

        private VoteInfoRepository voteInfoRepository;
        public VoteInfoRepository VoteInfoRepository
        {
            get
            {
                if (this.voteInfoRepository == null)
                {
                    this.voteInfoRepository = new VoteInfoRepository(context);
                }
                return voteInfoRepository;
            }
        }


        private VoteDetailRepository voteDetailRepository;
        public VoteDetailRepository VoteDetailRepository
        {
            get
            {
                if (this.voteDetailRepository == null)
                {
                    this.voteDetailRepository = new VoteDetailRepository(context);
                }
                return voteDetailRepository;
            }
        }

        private UserVoteRepository userVoteRepository;
        public UserVoteRepository UserVoteRepository
        {
            get
            {
                if (this.userVoteRepository == null)
                {
                    this.userVoteRepository = new UserVoteRepository(context);
                }
                return userVoteRepository;
            }
        }

        /// <summary>
        /// 保存当前上下文的修改
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            try
            {
                return context.SaveChanges();
            }
            //catch (DbEntityValidationException dbEx)
            catch (Exception dbEx)
            {
                throw new Exception(dbEx.Message);
                //foreach (var validationErrors in dbEx.EntityValidationErrors)
                //{
                //    foreach (var validationError in validationErrors.ValidationErrors)
                //    {
                //        throw new Exception(string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage));
                //    }
                //}
            }

        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
