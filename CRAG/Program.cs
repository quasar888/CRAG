
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRAG
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                //Console.Clear(); // Effacer la console à chaque nouvelle itération
                Console.WriteLine("Retrieval-Augmented Generation (RAG) Example");
                Console.WriteLine("Catégories de questions possibles :");
                Console.WriteLine("- Exemple de question a copie : capitale de la France, capitale du Canada");
                Console.WriteLine("- Exemple de question a copie : Tour Eiffel, Pyramides de Gizeh, Taj Mahal");
                Console.WriteLine("- Exemple de question a copie : bataille de Waterloo, Albert Einstein, Wright brothers");
                Console.WriteLine("- Exemple de question a copie : vitesse de la lumière, formule de l’eau, plus petit os du corps humain");
                Console.WriteLine("- Exemple de question a copie : Lune, Soleil, Jupiter, Vénus, système solaire");
                Console.WriteLine("- Exemple de question a copie : plus long fleuve, plus grand océan, plus haute montagne");
                Console.WriteLine("- Exemple de question a copie : Yen japonais, livre sterling britannique");
                Console.WriteLine("- Exemple de question a copie : Shakespeare, Picasso, Hamlet, Cubisme");
                Console.WriteLine("- Exemple de question a copie : téléphone, avion, Alexander Graham Bell");
                Console.WriteLine("- Exemple de question a copie : plus grand désert, plus grand récif corallien, plus grande île");

                // Input from the user
                Console.WriteLine("\nPosez votre question (ou tapez 'exit' pour quitter) :");
                string? question = Console.ReadLine();

                // Quitter le programme si l'utilisateur tape 'exit'////
                if (question != null)
                    if (question.Equals("exit", StringComparison.CurrentCultureIgnoreCase))
                    {
                        Console.WriteLine("Merci d'avoir utilisé le système RAG. À bientôt !");
                        break;
                    }

                // Expanded knowledge base with 50 entries
                var knowledgeBase = new List<Document>
                {
                    new Document { Id = 1, Content = "The capital of France is Paris." },
                    new Document { Id = 2, Content = "The Eiffel Tower is located in Paris." },
                    new Document { Id = 3, Content = "France is a country in Western Europe." },
                    new Document { Id = 4, Content = "The capital of Germany is Berlin." },
                    new Document { Id = 5, Content = "Berlin is famous for its Brandenburg Gate." },
                    new Document { Id = 6, Content = "The Great Wall of China is one of the Seven Wonders of the World." },
                    new Document { Id = 7, Content = "Mount Everest is the highest mountain in the world." },
                    new Document { Id = 8, Content = "The Amazon Rainforest is the largest rainforest on Earth." },
                    new Document { Id = 9, Content = "The Pacific Ocean is the largest ocean on Earth." },
                    new Document { Id = 10, Content = "Shakespeare wrote plays like Hamlet and Romeo and Juliet." },
                    new Document { Id = 11, Content = "The Moon orbits the Earth approximately every 27.3 days." },
                    new Document { Id = 12, Content = "The speed of light in vacuum is about 299,792 kilometers per second." },
                    new Document { Id = 13, Content = "The chemical symbol for water is H2O." },
                    new Document { Id = 14, Content = "Albert Einstein developed the theory of relativity." },
                    new Document { Id = 15, Content = "The human heart has four chambers." },
                    new Document { Id = 16, Content = "The Pyramids of Giza are in Egypt." },
                    new Document { Id = 17, Content = "The Statue of Liberty is in New York City." },
                    new Document { Id = 18, Content = "The currency of Japan is the Yen." },
                    new Document { Id = 19, Content = "The sun is a star located at the center of our solar system." },
                    new Document { Id = 20, Content = "The largest planet in our solar system is Jupiter." },
                    new Document { Id = 21, Content = "The smallest bone in the human body is the stapes." },
                    new Document { Id = 22, Content = "The inventor of the telephone was Alexander Graham Bell." },
                    new Document { Id = 23, Content = "The atomic number of carbon is 6." },
                    new Document { Id = 24, Content = "The longest river in the world is the Nile." },
                    new Document { Id = 25, Content = "The hardest natural substance on Earth is diamond." },
                    new Document { Id = 26, Content = "The capital of Canada is Ottawa." },
                    new Document { Id = 27, Content = "The Great Barrier Reef is the world's largest coral reef system." },
                    new Document { Id = 28, Content = "The tallest building in the world is the Burj Khalifa." },
                    new Document { Id = 29, Content = "The national animal of Australia is the kangaroo." },
                    new Document { Id = 30, Content = "The currency used in the UK is the British Pound." },
                    new Document { Id = 31, Content = "The country with the largest population is China." },
                    new Document { Id = 32, Content = "The first president of the United States was George Washington." },
                    new Document { Id = 33, Content = "The largest desert in the world is the Antarctic Desert." },
                    new Document { Id = 34, Content = "The boiling point of water at sea level is 100°C." },
                    new Document { Id = 35, Content = "The Earth takes approximately 365.25 days to orbit the sun." },
                    new Document { Id = 36, Content = "The capital of Italy is Rome." },
                    new Document { Id = 37, Content = "The primary gas in Earth's atmosphere is nitrogen." },
                    new Document { Id = 38, Content = "The Taj Mahal is in India." },
                    new Document { Id = 39, Content = "Venus is the hottest planet in the solar system." },
                    new Document { Id = 40, Content = "The chemical formula for table salt is NaCl." },
                    new Document { Id = 41, Content = "The largest island in the world is Greenland." },
                    new Document { Id = 42, Content = "The longest reigning British monarch was Queen Elizabeth II." },
                    new Document { Id = 43, Content = "The Battle of Waterloo took place in 1815." },
                    new Document { Id = 44, Content = "The deepest ocean trench in the world is the Mariana Trench." },
                    new Document { Id = 45, Content = "The primary ingredient in chocolate is cocoa." },
                    new Document { Id = 46, Content = "The first man to walk on the Moon was Neil Armstrong." },
                    new Document { Id = 47, Content = "The capital of Brazil is Brasília." },
                    new Document { Id = 48, Content = "The Amazon River is the second longest river in the world." },
                    new Document { Id = 49, Content = "The Spanish painter Pablo Picasso co-founded the Cubist movement." },
                    new Document { Id = 50, Content = "The Wright brothers invented the airplane." }
                };

                // Step 1: Retrieval - Find relevant documents
                var retrievedDocs = RetrieveRelevantDocuments(question, knowledgeBase);

                // Step 2: Generation - Create a response
                string response = GenerateResponse(question, retrievedDocs);

                Console.WriteLine("\nGenerated Response:");
                Console.WriteLine(response);
                //Thread.Sleep(1000);
                // Attendre que l'utilisateur appuie sur une touche avant de continuer
                Console.WriteLine("\nAppuyez sur une touche pour continuer...");
                Console.ReadKey();
            }
        }

        // Document class to represent knowledge base entries
        class Document
        {
            public int Id { get; set; }
            public string? Content { get; set; }
        }

        // Step 1: Retrieval
        // Step 1: Retrieval - Version améliorée
        static List<Document> RetrieveRelevantDocuments(string query, List<Document> knowledgeBase)
        {
            // Normalisation avancée de la requête
            var normalizedQuery = new string(query
                .Where(c => !char.IsPunctuation(c))
                .ToArray())
                .ToLower()
                .Normalize(System.Text.NormalizationForm.FormD); // Décomposition des caractères accentués

            var keywords = normalizedQuery.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // Dictionnaire étendu de synonymes et termes associés
            var keywordVariants = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
    {
        // Capitales et géographie
        { "capitale", new List<string> { "capital", "city", "principal", "administrative center" } },
        { "ville", new List<string> { "city", "town", "metropolis", "urban area" } },
        { "pays", new List<string> { "country", "nation", "state", "realm", "kingdom" } },
        
        // Monuments et sites
        { "monument", new List<string> { "tower", "statue", "building", "landmark", "heritage site" } },
        { "site", new List<string> { "location", "place", "area", "UNESCO" } },
        
        // Histoire
        { "histoire", new List<string> { "history", "historical", "war", "battle", "king", "president" } },
        { "bataille", new List<string> { "battle", "war", "conflict", "combat" } },
        
        // Science
        { "science", new List<string> { "physics", "chemistry", "biology", "scientific", "element" } },
        { "physique", new List<string> { "physics", "quantum", "relativity", "einstein" } },
        
        // Astronomie
        { "astronomie", new List<string> { "astronomy", "planet", "star", "galaxy", "solar system" } },
        { "planète", new List<string> { "planet", "celestial body", "orb", "jupiter", "mars" } },
        
        // Nature
        { "nature", new List<string> { "nature", "geography", "environment", "ecosystem" } },
        { "fleuve", new List<string> { "river", "stream", "nile", "amazon" } },
        { "montagne", new List<string> { "mountain", "peak", "everest", "alps" } },
        
        // Économie
        { "économie", new List<string> { "economy", "currency", "money", "gdp", "market" } },
        { "devise", new List<string> { "currency", "money", "yen", "dollar", "euro" } },
        
        // Culture
        { "culture", new List<string> { "culture", "art", "painting", "literature", "shakespeare" } },
        { "art", new List<string> { "art", "painting", "sculpture", "picasso", "cubism" } },
        
        // Technologie
        { "technologie", new List<string> { "technology", "invention", "innovation", "patent", "engineer" } },
        { "invention", new List<string> { "invention", "discovery", "creation", "wright", "bell" } },
        
        // Records
        { "record", new List<string> { "record", "largest", "highest", "longest", "biggest" } },
        { "superlatif", new List<string> { "superlative", "most", "greatest", "extreme", "ultimate" } }
    };

            // Expansion des mots-clés avec variantes et lemmatisation
            var expandedKeywords = keywords
                .SelectMany(keyword =>
                {
                    var baseKeyword = keyword.ToLower().Trim();
                    var variants = new List<string> { baseKeyword };

                    if (keywordVariants.ContainsKey(baseKeyword))
                    {
                        variants.AddRange(keywordVariants[baseKeyword]);
                    }

                    // Gestion des pluriels simples
                    if (baseKeyword.EndsWith("s") && baseKeyword.Length > 1)
                    {
                        var singular = baseKeyword[..^1];
                        variants.Add(singular);
                    }
                    else
                    {
                        variants.Add(baseKeyword + "s");
                    }

                    return variants.Distinct();
                })
                .ToList();

            // Recherche pondérée avec priorité aux correspondances exactes
            var relevantDocs = knowledgeBase
                .Select(doc =>
                {
                    var content = doc.Content.ToLower();
                    var score = expandedKeywords.Sum(keyword =>
                    {
                        // Bonus pour les correspondances exactes
                        if (content.Contains($" {keyword} ") || content.StartsWith(keyword + " ") || content.EndsWith(" " + keyword))
                            return 2;

                        return content.Contains(keyword) ? 1 : 0;
                    });

                    return new { Doc = doc, Score = score };
                })
                .Where(x => x.Score > 0)
                .OrderByDescending(x => x.Score)
                .Take(5) // Augmentation à 5 résultats maximum
                .Select(x => x.Doc)
                .ToList();

            return relevantDocs;
        }
        // Step 2: Generation
        static string GenerateResponse(string query, List<Document> retrievedDocs)
        {
            if (!retrievedDocs.Any())
            {
                return "Je suis désolé, mais je n'ai pas trouvé d'information pertinente.";
            }

            // Build a coherent response
            var response = "D'après mes connaissances, voici ce que je peux dire :\n-";
            response += string.Join("\n-", retrievedDocs.Select(doc => doc.Content));//

            return response;
        }
    }
}