using System.Security.Cryptography;
using System.Text;
using HotelManagement.Application.IServices;
using HotelManagement.Application.Models.Dtos.RequestDtos;
using HotelManagement.Application.Models.Dtos.ResponseDtos;
using HotelManagement.Domain.Entities;
using HotelManagement.Domain.Models.Constants;
using HotelManagement.Domain.Models.Enums;
using HotelManagement.Infrastructure.Email;
using HotelManagement.Infrastructure.Repository;
using HotelManagement.Infrastructure.Token;
using Microsoft.AspNetCore.Identity;
//Todo: add unit of work support
//Todo: Add more exception support instead of Exception only (NotFoundException, ArgumentException...)
namespace HotelManagement.Application.Services
{
    public class AuthenticationService(
        IRepository<User> userRepository,
        IRepository<EmailConfirmation> confirmEmailRepository,
        ITokenService tokenService,
        IEmailService emailService,
        IRepository<RefreshToken> refreshTokenRepository,
        IRepository<UserRole> userRoleRepository,
        UserManager<User> userManager)
        : IAuthenticationService
    {
        public async Task<BaseResponseDto<LoginResponseDto>> AdminLogin(LoginRequestDto loginDto)
        {
            var user = await userRepository.GetOneAsync(
                filter: u => u.Email == loginDto.LoginEmail);

            if (user == null)
            {
                return new BaseResponseDto<LoginResponseDto>
                {
                    Status = 404,
                    Message = "Email không tồn tại.",
                    ResponseData = null
                };
            }

            var passwordHasher = new PasswordHasher<User>();
            var verificationResult = passwordHasher.VerifyHashedPassword(
                user, user.PasswordHash ?? "", loginDto.Password);

            if (verificationResult != PasswordVerificationResult.Success)
            {
                return new BaseResponseDto<LoginResponseDto>
                {
                    Status = 401,
                    Message = "Sai mật khẩu.",
                    ResponseData = null
                };
            }

            if (user.UserStatus != UserStatus.Active)
            {
                return new BaseResponseDto<LoginResponseDto>
                {
                    Status = 400,
                    Message = "Tài khoản chưa được kích hoạt.",
                    ResponseData = null
                };
            }

            var userRoles = await userManager.GetRolesAsync(user);

            if (!userRoles.Contains("Admin"))
            {
                return new BaseResponseDto<LoginResponseDto>
                {
                    Status = 403,
                    Message = "Bạn không có quyền truy cập trang quản trị.",
                    ResponseData = null
                };
            }

            var accessToken = await tokenService.CreateAccessTokenAsync(user);
            var refreshToken = await tokenService.CreateRefreshTokenAsync(user);

            return new BaseResponseDto<LoginResponseDto>
            {
                Status = 200,
                Message = "Đăng nhập admin thành công.",
                ResponseData = new LoginResponseDto
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                }
            };
        }

        public async Task<BaseResponseDto<bool>> ConfirmEmail(ConfirmEmailRequestDto request)
        {
            var user = await userRepository.GetOneAsync(u => u.Email == request.ConfirmEmail);
            if (user == null)
            {
                return new BaseResponseDto<bool>
                {
                    Status = 404,
                    Message = "User not found.",
                    ResponseData = false
                };
            }

            var confirmEmail = await confirmEmailRepository.GetOneAsync(
                c => c.UserId == user.Id && c.ConfirmCode == request.ConfirmCode);

            if (confirmEmail == null || confirmEmail.ExpiresAt < DateTime.UtcNow)
            {
                return new BaseResponseDto<bool>
                {
                    Status = 400,
                    Message = "Invalid or expired confirmation code.",
                    ResponseData = false
                };
            }

            confirmEmail.IsConfirmed = true;

            user.UserStatus = UserStatus.Active;

            await userRepository.UpdateAsync(user);

            await confirmEmailRepository.UpdateAsync(confirmEmail);

            return new BaseResponseDto<bool>
            {
                Status = 200,
                Message = "Email confirmed successfully.",
                ResponseData = true
            };
        }

        public async Task<BaseResponseDto<LoginResponseDto>> Login(LoginRequestDto request)
        {
            var user = await userRepository.GetOneAsync(filter: f => f.Email == request.LoginEmail);
            if (user == null)
            {
                return new BaseResponseDto<LoginResponseDto>
                {
                    Status = 404,
                    Message = "Email does not exist, please try again.",
                    ResponseData = null
                };
            }
            var passwordHasher = new PasswordHasher<User>();
            var verificationResult = passwordHasher.VerifyHashedPassword(
                user, user.PasswordHash ?? "", request.Password);

            if (verificationResult != PasswordVerificationResult.Success)
            {
                return new BaseResponseDto<LoginResponseDto>
                {
                    Status = 401,
                    Message = "Password does not match, please try again.",
                    ResponseData = null
                };
            }

            if (user.UserStatus != UserStatus.Active)
            {
                return new BaseResponseDto<LoginResponseDto>
                {
                    Status = 400,
                    Message = "Please check your account status.",
                    ResponseData = null
                };
            }

            var accessToken = await tokenService.CreateAccessTokenAsync(user);
            var refreshToken = await tokenService.CreateRefreshTokenAsync(user);

            return new BaseResponseDto<LoginResponseDto>
            {
                Status = 200,
                Message = "Login successfully.",
                ResponseData = new LoginResponseDto
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                }
            };
        }

        public async Task<BaseResponseDto<RefreshTokenResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto request)
        {
            var refreshToken = await refreshTokenRepository.GetOneAsync(
                filter: f => f.Token == request.RefreshToken && f.ExpiredTime > DateTime.UtcNow);

            if (refreshToken == null)
            {
                return new BaseResponseDto<RefreshTokenResponseDto>
                {
                    Status = 401,
                    Message = "Invalid refresh token",
                    ResponseData = null
                };
            }
            
            var userId = refreshToken.UserId;

            var user = await userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                return new BaseResponseDto<RefreshTokenResponseDto>
                {
                    Status = 401,
                    Message = "Invalid refresh token",
                    ResponseData = null
                };
            }

            var accessToken = await tokenService.CreateAccessTokenAsync(user);
            var newRefreshToken = await tokenService.CreateRefreshTokenAsync(user);

            return new BaseResponseDto<RefreshTokenResponseDto>
            {
                Status = 200,
                Message = "Token refreshed",
                ResponseData = new RefreshTokenResponseDto
                {
                    AccessToken = accessToken,
                    RefreshToken = newRefreshToken
                }
            };
        }


        public async Task<BaseResponseDto<bool>> Register(RegisterRequestDto request)
        {
            using var transaction = await userRepository.BeginTransactionAsync();
            try
            {
                var existingUser = await userRepository.GetOneAsync(u => u.Email == request.Email);
                if (existingUser != null)
                {
                    return new BaseResponseDto<bool>
                    {
                        Status = 400,
                        Message = "Email is already taken.",
                        ResponseData = false
                    };
                }

                var newUser = new User
                {
                    Id = Guid.NewGuid(),
                    NormalizedEmail = request.Email.ToUpper(),
                    NormalizedUserName = $"{request.FirstName} {request.LastName}".ToUpper(),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Email = request.Email,
                    UserName = request.UserName,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    DateOfBirth = request.DateOfBirth,
                    Gender = request.Gender,
                    PhoneNumber = request.PhoneNumber,
                    AvatarUrl = "images/user/DefaultAvatar.jpg",
                    UserStatus = UserStatus.Pending,
                };

                var passwordHasher = new PasswordHasher<User>();
                newUser.PasswordHash = passwordHasher.HashPassword(newUser, request.Password);

                await userRepository.AddAsync(newUser);

                await userRepository.CommitTransactionAsync(transaction);

                return new BaseResponseDto<bool>
                {
                    Status = 200,
                    Message = "Đăng ký thành công, hãy kích hoạt tài khoản của bạn.",
                    ResponseData = true
                };
            }
            catch (Exception ex)
            {
                await userRepository.RollbackTransactionAsync(transaction);
                return new BaseResponseDto<bool>
                {
                    Status = 500,
                    Message = "An error occurred during registration.",
                    ResponseData = false
                };
            }
        }

        public async Task<BaseResponseDto<bool>> SendConfirmationCodeAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return new BaseResponseDto<bool>
                {
                    Status = 400,
                    Message = "Email cannot be empty.",
                    ResponseData = false
                };
            }

            var user = await userRepository.GetOneAsync(u => u.Email == email);
            if (user == null)
            {
                return new BaseResponseDto<bool>
                {
                    Status = 404,
                    Message = "User not found.",
                    ResponseData = false
                };
            }

            if (user.UserStatus == UserStatus.Active)
            {
                return new BaseResponseDto<bool>
                {
                    Status = 400,
                    Message = "User already activated.",
                    ResponseData = false
                };
            }

            var oldCodes = await confirmEmailRepository.GetListAsync(
                c => c.UserId == user.Id && !c.IsConfirmed && c.ExpiresAt > DateTime.UtcNow
            );

            foreach (var old in oldCodes)
            {
                old.IsConfirmed = true;
            }

            await confirmEmailRepository.UpdateRangeAsync(oldCodes);

            string confirmCode = GenerateVerificationCode(8);

            var confirmEmail = new EmailConfirmation
            {
                UserId = user.Id,
                ConfirmCode = confirmCode,
                RequestedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(10),
                IsConfirmed = false
            };

            await confirmEmailRepository.AddAsync(confirmEmail);

            string subject = "Xác thực tài khoản HopeBox";
            string body = $"Mã xác thực của bạn là: <strong>{confirmCode}</strong>";

            await emailService.SendEmailAsync(user.Email, subject, body);

            return new BaseResponseDto<bool>
            {
                Status = 200,
                Message = "Confirmation code sent successfully.",
                ResponseData = true
            };
        }

        private string GenerateVerificationCode(int length)
        {
            var AllChars = (Constant.DigitChars).ToCharArray();
            var result = new StringBuilder(length);
            using (var rng = RandomNumberGenerator.Create())
            {
                var bytes = new byte[length];

                rng.GetBytes(bytes);

                for (int i = 0; i < length; i++)
                {
                    int index = bytes[i] % AllChars.Length;
                    result.Append(AllChars[index]);
                }
            }
            return result.ToString();
        }
    }
}
