using AutoMapper;
using RestApiNetCoreDemo.DAL.DTOs;
using RestApiNetCoreDemo.DAL.Models;

namespace RestApiNetCoreDemo.Configurations
{
    public class MapperConfig:Profile
    {
        public MapperConfig()
        {
            CreateMap<TodoItem, TodoItemDTO>();
        }
    }
}
