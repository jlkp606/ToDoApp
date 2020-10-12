using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todoapp.Models;

namespace Todoapp.Database
{
    public class TodoDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public TodoDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        public SQLiteAsyncConnection DatabaseInstance 
        {
			get
			{
                if (!initialized)
                    new TodoDatabase();
                return Database;
            }
        }
        async Task InitializeAsync()
        {
           
           
            if (!initialized)
            {
                //// for debug
                //if (Database.TableMappings.Any(m => m.MappedType.Name == typeof(User).Name))
                //{
                //    await Database.DropTableAsync<User>().ConfigureAwait(false);
                //}
                //await Database.CreateTablesAsync(CreateFlags.None, typeof(User)).ConfigureAwait(false);

                //if (Database.TableMappings.Any(m => m.MappedType.Name == typeof(Item).Name))
                //{
                //    await Database.DropTableAsync<Item>().ConfigureAwait(false);
                //}
                //await Database.CreateTablesAsync(CreateFlags.None, typeof(Item)).ConfigureAwait(false);

                //for prod
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(User).Name))
                    {
                        await Database.CreateTablesAsync(CreateFlags.None, typeof(User)).ConfigureAwait(false);
                    }


                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Item).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Item)).ConfigureAwait(false);
                }

               

                initialized = true;
            }
        }
    }
}

public static class TaskExtensions
{
    // NOTE: Async void is intentional here. This provides a way
    // to call an async method from the constructor while
    // communicating intent to fire and forget, and allow
    // handling of exceptions
    public static async void SafeFireAndForget(this Task task,
        bool returnToCallingContext,
        Action<Exception> onException = null)
    {
        try
        {
            await task.ConfigureAwait(returnToCallingContext);
        }

        // if the provided action is not null, catch and
        // pass the thrown exception
        catch (Exception ex) when (onException != null)
        {
            onException(ex);
        }
    }
}
