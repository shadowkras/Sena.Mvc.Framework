﻿using System.Threading.Tasks;

namespace Vigente.Framework.Data.Interfaces
{
    public interface IGenericUnitOfWork
    {
        /// <summary>
        /// Start a new transaction.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Save changes to our database.
        /// </summary>
        bool Commit();

        /// <summary>
        /// Save changes to our database.
        /// </summary>
        Task<bool> CommitAsync();

        /// <summary>
        /// Return whether the Commit statement was successful.
        /// </summary>
        /// <returns></returns>
        bool IsSucess();

        /// <summary>
        /// Returns the operation message.
        /// </summary>
        /// <returns></returns>
        string GetOperationMessage();
    }
}