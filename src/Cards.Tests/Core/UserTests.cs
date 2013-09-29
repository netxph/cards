using Speculous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Cards.Core;
using Moq;
using Moq.Protected;

namespace Cards.Tests.Core
{
    public class UserTests
    {
        public class Initialize : TestCase<User>
        {
            readonly DateTime NOW = new DateTime(2013, 9, 12);

            protected override Func<User> Given()
            {
                var date = new Mock<IDateProvider>();

                date.Setup(d => d.UtcNow()).Returns(NOW);

                User.DateProvider = date.Object;

 	            return () => new User();
            }
            
            [Fact]
            public void ShouldNotReturnNull()
            {
                Subject().Should().NotBeNull();
            }

            [Fact]
            public void ShouldUserIDReturnZero()
            {
                Its.ID.Should().Be(0);
            }

            [Fact]
            public void ShouldAliasIsNull()
            {
                Its.Alias.Should().BeNull();
            }

            [Fact]
            public void ShouldUserNameBeNull()
            {
                Its.Name.Should().BeNull();
            }

            [Fact]
            public void ShouldEmailBeNull()
            {
                Its.Email.Should().BeNull();
            }

            [Fact]
            public void ShouldCreatedIsNow()
            {
                Its.CreatedDateUtc.Should().Be(NOW);
            }

            [Fact]
            public void ShouldModifiedIsNow()
            {
                Its.ModifiedDateUtc.Should().Be(NOW);
            }

            [Fact]
            public void ShouldBeActive()
            {
                Its.IsActive.Should().BeTrue();
            }

        }

        public class InitializeUserWithData : TestCase<User>
        {
            readonly DateTime NOW = DateTime.Now;

            protected override Func<User> Given()
            {
                var dateProvider = new Mock<IDateProvider>();

                dateProvider.Setup(d => d.UtcNow()).Returns(NOW);

                User.DateProvider = dateProvider.Object;

                return () => new User()
                    {
                        ID = 1,
                        Email = "email@email.com",
                        Name = "Hello, World!"
                    };
            }

            [Fact]
            public void ShouldUserBeNull()
            {
                Subject.Should().NotBeNull();
            }

            [Fact]
            public void ShouldIDBeOne()
            {
                Its.ID.Should().Be(1);
            }

            [Fact]
            public void ShouldEmailHaveValue()
            {
                Its.Email.Should().Be("email@email.com");
            }

            [Fact]
            public void ShouldNameHaveValue()
            {
                Its.Name.Should().Be("Hello, World!");
            }

            [Fact]
            public void ShouldCreatedDateBeNow()
            {
                Its.CreatedDateUtc.Should().Be(NOW);
            }

            [Fact]
            public void ShouldModifiedDateBeNow()
            {
                Its.ModifiedDateUtc.Should().Be(NOW);
            }

            [Fact]
            public void ShouldBeActive()
            {
                Its.IsActive.Should().BeTrue();
            }

        }

        public class AddUserMethod : TestCase<User>
        {
            readonly DateTime NOW = DateTime.Now;

            protected override Func<User> Given()
            {
                User user = null;

                var dateProvider = new Mock<IDateProvider>();

                dateProvider.Setup(d => d.UtcNow()).Returns(NOW);

                User.DateProvider = dateProvider.Object;

                var repository = new Mock<ICardRepository>();
                repository
                            .Setup(r => r.CreateUser(It.IsAny<User>()))
                            .Callback<User>(u => user = u)
                            .Returns(() => user);

                var factory = new Mock<DbFactory>();
                factory
                        .Protected()
                        .Setup<ICardRepository>("OnCreateDb")
                        .Returns(repository.Object);

                new DbFactory(factory.Object);

                return () => User.Register("user@website.com", "user");
            }

            [Fact]
            public void UserShouldNotBeNull()
            {
                Subject.Should().NotBeNull();
            }

            [Fact]
            public void UserNameShouldBeUser()
            {
                Its.Name.Should().Be("user");
            }

            [Fact]
            public void UserEmailShouldHaveValue()
            {
                Its.Email.Should().Be("user@website.com");
            }

            [Fact]
            public void ShouldCreatedDateBeNow()
            {
                Its.CreatedDateUtc.Should().Be(NOW);
            }

            [Fact]
            public void ShouldModifiedDateBeNow()
            {
                Its.ModifiedDateUtc.Should().Be(NOW);
            }
        }

        public class GetUserMethod : TestCase<User>
        {
            DateTime CREATED = new DateTime(2013, 9, 1);
            DateTime NOW = new DateTime(2013, 9, 6);

            protected override Func<User> Given()
            {
                var dateProvider = new Mock<IDateProvider>();

                dateProvider.Setup(d => d.UtcNow()).Returns(NOW);

                User.DateProvider = dateProvider.Object;

                var repository = new Mock<ICardRepository>();

                repository
                            .Setup(r => r.FindUser("user@website.com"))
                            .Returns(new User()
                            {
                                ID              = 1,
                                Name            = "user",
                                Email           = "user@website.com",
                                CreatedDateUtc  = CREATED,
                                ModifiedDateUtc = CREATED
                            });

                var factory = new Mock<DbFactory>();

                factory.Protected()
                       .Setup<ICardRepository>("OnCreateDb")
                       .Returns(repository.Object);

                new DbFactory(factory.Object);

                return () => User.CheckRegistration("user@website.com");
            }

            [Fact]
            public void ShouldUserBeNull()
            {
                Subject.Should().NotBeNull();
            }

            [Fact]
            public void ShouldUserIDBeOne()
            {
                Its.ID.Should().Be(1);
            }

            [Fact]
            public void ShouldUserNameBeUser()
            {
                Its.Name.Should().Be("user");
            }

            [Fact]
            public void ShouldUserEmailHaveValue()
            {
                Its.Email.Should().Be("user@website.com");
            }

            [Fact]
            public void ShouldCreatedTimeHaveValue()
            {
                Its.CreatedDateUtc.Should().Be(CREATED);
            }

            [Fact]
            public void ShouldCreatedTimeNotBeNow()
            {
                Its.CreatedDateUtc.Should().NotBe(NOW);
            }

            [Fact]
            public void ShouldModifiedTimeHaveValue()
            {
                Its.CreatedDateUtc.Should().Be(CREATED);
            }

            [Fact]
            public void ShouldModifiedTimeNotBeNow()
            {
                Its.CreatedDateUtc.Should().NotBe(NOW);
            }
        }

        public class UpdateUserMethod : TestCase<User>
        {
            readonly DateTime NOW = DateTime.Now;

            protected override Func<User> Given()
            {
                var user = new User()
                {
                    ID              = 1,
                    Name            = "name",
                    Email           = "email@email.com",
                    ModifiedDateUtc = DateTime.MinValue,
                    CreatedDateUtc  = DateTime.MinValue
                };

                var dateProvider = new Mock<IDateProvider>();

                dateProvider.Setup(d => d.UtcNow()).Returns(NOW);

                User.DateProvider = dateProvider.Object;

                var repository = new Mock<ICardRepository>();

                repository
                            .Setup(r => r.FindUser("email@email.com"))
                            .Returns(user);

                var factory = new Mock<DbFactory>();

                factory.Protected()
                       .Setup<ICardRepository>("OnCreateDb")
                       .Returns(repository.Object);

                new DbFactory(factory.Object);

                return () => User.Update("email@email.com", "updatedName");
            }

            [Fact]
            public void UpdatedUserShouldNotBeNull()
            {
                Subject.Should().NotBeNull();
            }

            [Fact]
            public void UpdatedUserNameShouldBeUpdatedName()
            {
                Its.Name.Should().Be("updatedName");
            }

            [Fact]
            public void UpdatedUserEmailShouldHaveValue()
            {
                Its.Email.Should().Be("email@email.com");
            }

            [Fact]
            public void UpdatedUserModifiedDateShouldBeNow()
            {
                Its.ModifiedDateUtc.Should().Be(NOW);
            }

            [Fact]
            public void UpdatedUserIDShouldBeOne()
            {
                Its.ID.Should().Be(1);
            }
        }

        public class DeleteUserMethod : TestCase<User>
        {
            readonly DateTime NOW = DateTime.Now;

            protected override Func<User> Given()
            {
                var user = new User()
                {
                    ID    = 1,
                    Name  = "toDelete",
                    Email = "delete@email.com",
                };

                var dateProvider = new Mock<IDateProvider>();

                dateProvider.Setup(d => d.UtcNow()).Returns(NOW);

                User.DateProvider = dateProvider.Object;

                var repository = new Mock<ICardRepository>();

                repository
                            .Setup(r => r.FindUser("delete@email.com"))
                            .Returns(user);

                var factory = new Mock<DbFactory>();

                factory.Protected()
                       .Setup<ICardRepository>("OnCreateDb")
                       .Returns(repository.Object);

                new DbFactory(factory.Object);

                return () => User.Delete("delete@email.com");
            }

            [Fact]
            public void ShouldUserBeNull()
            {
                Subject.Should().NotBeNull();
            }

            [Fact]
            public void ShouldUserNameBeToDelete()
            {
                Its.Name.Should().Be("toDelete");
            }

            [Fact]
            public void ShouldUserEmailHaveValue()
            {
                Its.Email.Should().Be("delete@email.com");
            }

            [Fact]
            public void ShouldUserModifiedDateBeNow()
            {
                Its.ModifiedDateUtc.Should().Be(NOW);
            }

            [Fact]
            public void ShouldUserBeInactive()
            {
                Its.IsActive.Should().BeFalse();
            }
        }
    }
}
