
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AGI.DataManagement
{
    public class DataManager
    {
        private readonly KnowledgeGraphContext _context;

        public DataManager(KnowledgeGraphContext context)
        {
            _context = context;
        }

        // Method to add a KnowledgeNode
        public async Task AddKnowledgeNodeAsync(string name, string description)
        {
            var node = new KnowledgeNode { Name = name, Description = description };
            _context.KnowledgeNodes.Add(node);
            await _context.SaveChangesAsync();
        }

        // Method to retrieve all KnowledgeNodes
        public async Task<List<KnowledgeNode>> GetKnowledgeNodesAsync()
        {
            return await _context.KnowledgeNodes.ToListAsync();
        }

        // Method to add an Experience
        public async Task AddExperienceAsync(string eventDescription, DateTime timestamp)
        {
            var experience = new Experience { Event = eventDescription, Timestamp = timestamp };
            _context.Experiences.Add(experience);
            await _context.SaveChangesAsync();
        }

        // Method to retrieve all Experiences
        public async Task<List<Experience>> GetExperiencesAsync()
        {
            return await _context.Experiences.ToListAsync();
        }
    }
}
