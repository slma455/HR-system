﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace HrProject.Filter
{
	public class PermissionPolicyProvider : IAuthorizationPolicyProvider
	{
		public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }
		public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
		{
			FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
		}
		public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
		{
			return FallbackPolicyProvider.GetDefaultPolicyAsync();
		}

		public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
		{
			return FallbackPolicyProvider.GetDefaultPolicyAsync();
		}

		public async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
		{
			if (policyName.StartsWith("Permission", StringComparison.OrdinalIgnoreCase))
			{
				var policy = new AuthorizationPolicyBuilder();
				policy.AddRequirements(new PermissionRequirment(policyName));
				return await Task.FromResult(policy.Build());
			}
			return await FallbackPolicyProvider.GetPolicyAsync(policyName);
		}
	}
}
