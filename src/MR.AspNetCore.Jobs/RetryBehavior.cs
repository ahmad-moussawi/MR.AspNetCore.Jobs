﻿using System;

namespace MR.AspNetCore.Jobs
{
	/// <summary>
	/// Represents the retry behavior to be used.
	/// </summary>
	public class RetryBehavior
	{
		public static readonly int DefaultRetryCount;
		public static readonly Func<int, int> DefaultRetryInThunk;

		public static readonly RetryBehavior DefaultRetry;
		public static readonly RetryBehavior NoRetry;

		private static Random _random = new Random();

		private Func<int, int> _retryInThunk;

		static RetryBehavior()
		{
			DefaultRetryCount = 25;
			DefaultRetryInThunk = retries =>
				(int)Math.Round(Math.Pow(retries - 1, 4) + 15 + (_random.Next(30) * (retries)));

			DefaultRetry = new RetryBehavior(true);
			NoRetry = new RetryBehavior(false);
		}

		public RetryBehavior(bool retry)
			: this(retry, DefaultRetryCount, DefaultRetryInThunk)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RetryBehavior"/> class.
		/// </summary>
		/// <param name="retry">Whether to retry.</param>
		/// <param name="retryCount">The maximum retry count.</param>
		/// <param name="retryInThunk">The retry in function to use.</param>
		public RetryBehavior(bool retry, int retryCount, Func<int, int> retryInThunk)
		{
			if (retry)
			{
				if (retryCount < 0) throw new ArgumentOutOfRangeException(nameof(retryCount), "Can't be negative.");
			}

			Retry = retry;
			RetryCount = retryCount;
			_retryInThunk = retryInThunk ?? DefaultRetryInThunk;
		}

		public Random Random => _random;

		/// <summary>
		/// Gets whether to retry or disable retrying.
		/// </summary>
		public bool Retry { get; }

		/// <summary>
		/// Gets the maximum retry count.
		/// </summary>
		public int RetryCount { get; }

		/// <summary>
		/// Returns the seconds to delay before retrying again.
		/// </summary>
		/// <param name="retries">The current retry count.</param>
		/// <returns>The seconds to delay.</returns>
		public int RetryIn(int retries)
		{
			return _retryInThunk(retries);
		}
	}
}
