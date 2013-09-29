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
    public class AccountTests
    {
        public class Initialize : TestCase<Account>
        {
            readonly DateTime NOW = new DateTime(2013, 9, 12);

            protected override Func<Account> Given()
            {
                var date = new Mock<IDateProvider>();

                date.Setup(d => d.UtcNow()).Returns(NOW);

                Account.DateProvider = date.Object;

 	            return () => new Account();
            }
            
            [Fact]
            public void ShouldNotReturnNull()
            {
                Subject().Should().NotBeNull();
            }

            [Fact]
            public void ShouldAccountIDReturnZero()
            {
                Its.ID.Should().Be(0);
            }

            [Fact]
            public void ShouldAliasIsNull()
            {
                Its.Alias.Should().BeNull();
            }

            [Fact]
            public void ShouldAccountNameBeNull()
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

        public class GetAllMethod : TestCase<List<Account>>
        {
            protected override Func<List<Account>> Given()
            {
                var repository = new Mock<ICardRepository>();
                repository.Setup(d => d.FindAllAccounts())
                    .Returns(new List<Account>()
                    {
                        new Account()
                        {
                            ID = 1,
                            Name = "John Doe",
                            Alias = "John",
                            Email = "john.doe@company.com",
                            IsActive = true
                            
                        }
                    });

                var factory = new Mock<DbFactory>();

                factory.Protected()
                       .Setup<ICardRepository>("OnCreateDb")
                       .Returns(repository.Object);

                new DbFactory(factory.Object);

                return () => Account.GetAll();
            }

            [Fact]
            public void ShouldNotBeNull()
            {
                Subject().Should().NotBeNull();
            }

            [Fact]
            public void ShouldNotBeEmpty()
            {
                Subject().Should().NotBeEmpty();
            }

            [Fact]
            public void ShouldFirstItemHasValue()
            {
                Its[0].Alias.Should().Be("John");
            }
        }

        public class InitializeAccountWithData : TestCase<Account>
        {
            readonly DateTime NOW = DateTime.Now;

            protected override Func<Account> Given()
            {
                var dateProvider = new Mock<IDateProvider>();

                dateProvider.Setup(d => d.UtcNow()).Returns(NOW);

                Account.DateProvider = dateProvider.Object;

                return () => new Account()
                    {
                        ID = 1,
                        Email = "email@email.com",
                        Name = "Hello, World!"
                    };
            }

            [Fact]
            public void ShouldAccountBeNull()
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

        public class AddAccountMethod : TestCase<Account>
        {
            readonly DateTime NOW = DateTime.Now;

            protected override Func<Account> Given()
            {
                Account Account = null;

                var dateProvider = new Mock<IDateProvider>();

                dateProvider.Setup(d => d.UtcNow()).Returns(NOW);

                Account.DateProvider = dateProvider.Object;

                var repository = new Mock<ICardRepository>();
                repository
                            .Setup(r => r.CreateAccount(It.IsAny<Account>()))
                            .Callback<Account>(u => Account = u)
                            .Returns(() => Account);

                var factory = new Mock<DbFactory>();
                factory
                        .Protected()
                        .Setup<ICardRepository>("OnCreateDb")
                        .Returns(repository.Object);

                new DbFactory(factory.Object);

                return () => Account.Register("Account@website.com", "Account");
            }

            [Fact]
            public void AccountShouldNotBeNull()
            {
                Subject.Should().NotBeNull();
            }

            [Fact]
            public void AccountNameShouldBeAccount()
            {
                Its.Name.Should().Be("Account");
            }

            [Fact]
            public void AccountEmailShouldHaveValue()
            {
                Its.Email.Should().Be("Account@website.com");
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

        public class GetAccountMethod : TestCase<Account>
        {
            DateTime CREATED = new DateTime(2013, 9, 1);
            DateTime NOW = new DateTime(2013, 9, 6);

            protected override Func<Account> Given()
            {
                var dateProvider = new Mock<IDateProvider>();

                dateProvider.Setup(d => d.UtcNow()).Returns(NOW);

                Account.DateProvider = dateProvider.Object;

                var repository = new Mock<ICardRepository>();

                repository
                            .Setup(r => r.FindAccount("Account@website.com"))
                            .Returns(new Account()
                            {
                                ID              = 1,
                                Name            = "Account",
                                Email           = "Account@website.com",
                                CreatedDateUtc  = CREATED,
                                ModifiedDateUtc = CREATED
                            });

                var factory = new Mock<DbFactory>();

                factory.Protected()
                       .Setup<ICardRepository>("OnCreateDb")
                       .Returns(repository.Object);

                new DbFactory(factory.Object);

                return () => Account.CheckRegistration("Account@website.com");
            }

            [Fact]
            public void ShouldAccountBeNull()
            {
                Subject.Should().NotBeNull();
            }

            [Fact]
            public void ShouldAccountIDBeOne()
            {
                Its.ID.Should().Be(1);
            }

            [Fact]
            public void ShouldAccountNameBeAccount()
            {
                Its.Name.Should().Be("Account");
            }

            [Fact]
            public void ShouldAccountEmailHaveValue()
            {
                Its.Email.Should().Be("Account@website.com");
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

        public class UpdateAccountMethod : TestCase<Account>
        {
            readonly DateTime NOW = DateTime.Now;

            protected override Func<Account> Given()
            {
                var Account = new Account()
                {
                    ID              = 1,
                    Name            = "name",
                    Email           = "email@email.com",
                    ModifiedDateUtc = DateTime.MinValue,
                    CreatedDateUtc  = DateTime.MinValue
                };

                var dateProvider = new Mock<IDateProvider>();

                dateProvider.Setup(d => d.UtcNow()).Returns(NOW);

                Account.DateProvider = dateProvider.Object;

                var repository = new Mock<ICardRepository>();

                repository
                            .Setup(r => r.FindAccount("email@email.com"))
                            .Returns(Account);

                var factory = new Mock<DbFactory>();

                factory.Protected()
                       .Setup<ICardRepository>("OnCreateDb")
                       .Returns(repository.Object);

                new DbFactory(factory.Object);

                return () => Account.Update("email@email.com", "updatedName");
            }

            [Fact]
            public void UpdatedAccountShouldNotBeNull()
            {
                Subject.Should().NotBeNull();
            }

            [Fact]
            public void UpdatedAccountNameShouldBeUpdatedName()
            {
                Its.Name.Should().Be("updatedName");
            }

            [Fact]
            public void UpdatedAccountEmailShouldHaveValue()
            {
                Its.Email.Should().Be("email@email.com");
            }

            [Fact]
            public void UpdatedAccountModifiedDateShouldBeNow()
            {
                Its.ModifiedDateUtc.Should().Be(NOW);
            }

            [Fact]
            public void UpdatedAccountIDShouldBeOne()
            {
                Its.ID.Should().Be(1);
            }
        }

        public class DeleteAccountMethod : TestCase<Account>
        {
            readonly DateTime NOW = DateTime.Now;

            protected override Func<Account> Given()
            {
                var Account = new Account()
                {
                    ID    = 1,
                    Name  = "toDelete",
                    Email = "delete@email.com",
                };

                var dateProvider = new Mock<IDateProvider>();

                dateProvider.Setup(d => d.UtcNow()).Returns(NOW);

                Account.DateProvider = dateProvider.Object;

                var repository = new Mock<ICardRepository>();

                repository
                            .Setup(r => r.FindAccount("delete@email.com"))
                            .Returns(Account);

                var factory = new Mock<DbFactory>();

                factory.Protected()
                       .Setup<ICardRepository>("OnCreateDb")
                       .Returns(repository.Object);

                new DbFactory(factory.Object);

                return () => Account.Delete("delete@email.com");
            }

            [Fact]
            public void ShouldAccountBeNull()
            {
                Subject.Should().NotBeNull();
            }

            [Fact]
            public void ShouldAccountNameBeToDelete()
            {
                Its.Name.Should().Be("toDelete");
            }

            [Fact]
            public void ShouldAccountEmailHaveValue()
            {
                Its.Email.Should().Be("delete@email.com");
            }

            [Fact]
            public void ShouldAccountModifiedDateBeNow()
            {
                Its.ModifiedDateUtc.Should().Be(NOW);
            }

            [Fact]
            public void ShouldAccountBeInactive()
            {
                Its.IsActive.Should().BeFalse();
            }
        }
    }
}
