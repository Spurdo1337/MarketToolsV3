﻿using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Domain.Entities
{
    public class Company : Entity
    {
        public string Name { get; private set; }
        public string? Token { get; private set; }

        public string OwnerId { get; private set; }
        public Owner Owner { get; private set; }

        private readonly List<Subscription> _subscriptions = new();
        public IReadOnlyCollection<Subscription> Subscriptions => _subscriptions;

        private readonly List<Subscriber> _subscribers = new();
        public IReadOnlyCollection<Subscriber> Subscribers => _subscribers;

        protected Company()
        {
            Name = null!;
            OwnerId = null!;
            Owner = null!;
        }

        public Company(string name, string? token, string ownerId) : this()
        {
            Name = name;
            Token = token;
            OwnerId = ownerId;
        }
    }
}
