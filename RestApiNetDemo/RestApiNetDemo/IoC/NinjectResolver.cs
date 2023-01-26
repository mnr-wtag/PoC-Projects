using Ninject;
using Ninject.Extensions.ChildKernel;
using RestApiNetDemo.BLL.IServices;
using RestApiNetDemo.BLL.Services;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

namespace RestApiNetDemo.IoC
{
    public class NinjectResolver : IDependencyResolver
    {
        private readonly IKernel kernel;

        public NinjectResolver() : this(new StandardKernel())
        {
        }

        public NinjectResolver(IKernel ninjectKernel, bool scope = false)
        {
            kernel = ninjectKernel;
            if (!scope)
            {
                AddBindings(kernel);
            }
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectResolver(AddRequestBindings(new ChildKernel(kernel)), true);
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        public void Dispose()
        {
        }

        private void AddBindings(IKernel kernel)
        {
            // singleton and transient bindings go here
            kernel.Bind<IAdminService>().To<AdminService>().InSingletonScope();
            kernel.Bind<IAuthUserService>().To<AuthUserService>().InSingletonScope();
            kernel.Bind<ICourseService>().To<CourseService>().InSingletonScope();
            kernel.Bind<IDepartmentService>().To<DepartmentService>().InSingletonScope();
            kernel.Bind<IEnrollmentService>().To<EnrollmentService>().InSingletonScope();
            kernel.Bind<IStudentService>().To<StudentService>().InSingletonScope();
            kernel.Bind<ITeacherService>().To<TeacherService>().InSingletonScope();
            kernel.Bind<IUserProfileService>().To<UserProfileService>().InSingletonScope();
        }

        private IKernel AddRequestBindings(IKernel kernel)
        {
            AddBindings(kernel);

            return kernel;
        }
    }
}