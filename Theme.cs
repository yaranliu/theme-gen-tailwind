namespace ThemeGenerator;

public class Theme
{

    public string? Value(string color, string variant)
    {
        return TailwindPalette.FirstOrDefault(p => p.Name == color)?
            .Variants?.FirstOrDefault(v => v.Name == variant)?
            .Value;
    }

    public List<ColorDefinition> CustomPalette { get; set; }

    public Theme()
    {
        CustomPalette = new List<ColorDefinition>();
    }
    public static IEnumerable<PaletteColor> TailwindPalette => new List<PaletteColor>()
    {
        new() {
            Name = "slate",
            Variants = new List<ColorVariant> () {
                new ("50", "#F8FAFC"),
                new ("100", "#F1F5F9"),
                new ("200", "#E2E8F0"),
                new ("300", "#CBD5E1"),
                new ("400", "#94A3B8"),
                new ("500", "#64748B"),
                new ("600", "#475569"),
                new ("700", "#334155"),
                new ("800", "#1E293B"),
                new ("900", "#0F172A")
            },
        },
        new PaletteColor() {
            Name = "gray",
            Variants = new List<ColorVariant> () {
                new ("50", "#F9FAFB"),
                new ("100", "#F3F4F6"),
                new ("200", "#E5E7EB"),
                new ("300", "#D1D5DB"),
                new ("400", "#9CA3AF"),
                new ("500", "#6B7280"),
                new ("600", "#4B5563"),
                new ("700", "#374151"),
                new ("800", "#1F2937"),
                new ("900", "#111827")
            },
        },
        new PaletteColor() {
            Name = "zinc",
            Variants = new List<ColorVariant> () {
                new ("50", "#FAFAFA"),
                new ("100", "#F4F4F5"),
                new ("200", "#E4E4E7"),
                new ("300", "#D4D4D8"),
                new ("400", "#A1A1AA"),
                new ("500", "#71717A"),
                new ("600", "#52525B"),
                new ("700", "#3F3F46"),
                new ("800", "#27272A"),
                new ("900", "#18181B")
            },
        },
        new PaletteColor() {
            Name = "neutral",
            Variants = new List<ColorVariant> () {
                new ("50", "#FAFAFA"),
                new ("100", "#F5F5F5"),
                new ("200", "#E5E5E5"),
                new ("300", "#D4D4D4"),
                new ("400", "#A3A3A3"),
                new ("500", "#737373"),
                new ("600", "#525252"),
                new ("700", "#404040"),
                new ("800", "#262626"),
                new ("900", "#171717")
            },
        },
        new PaletteColor() {
            Name = "stone",
            Variants = new List<ColorVariant> () {
                new ("50", "#FAFAF9"),
                new ("100", "#F5F5F4"),
                new ("200", "#E7E5E4"),
                new ("300", "#D6D3D1"),
                new ("400", "#A8A29E"),
                new ("500", "#78716C"),
                new ("600", "#57534E"),
                new ("700", "#44403C"),
                new ("800", "#292524"),
                new ("900", "#1C1917")
            },
        },
        new PaletteColor() {
            Name = "red",
            Variants = new List<ColorVariant> () {
                new ("50", "#FEF2F2"),
                new ("100", "#FEE2E2"),
                new ("200", "#FECACA"),
                new ("300", "#FCA5A5"),
                new ("400", "#F87171"),
                new ("500", "#EF4444"),
                new ("600", "#DC2626"),
                new ("700", "#B91C1C"),
                new ("800", "#991B1B"),
                new ("900", "#7F1D1D")
            },
        },
        new PaletteColor() {
            Name = "orange",
            Variants = new List<ColorVariant> () {
                new ("50", "#FFF7ED"),
                new ("100", "#FFEDD5"),
                new ("200", "#FED7AA"),
                new ("300", "#FDBA74"),
                new ("400", "#FB923C"),
                new ("500", "#F97316"),
                new ("600", "#EA580C"),
                new ("700", "#C2410C"),
                new ("800", "#9A3412"),
                new ("900", "#7C2D12")
            },
        },
        new PaletteColor() {
            Name = "amber",
            Variants = new List<ColorVariant> () {
                new ("50", "#FFFBEB"),
                new ("100", "#FEF3C7"),
                new ("200", "#FDE68A"),
                new ("300", "#FCD34D"),
                new ("400", "#FBBF24"),
                new ("500", "#F59E0B"),
                new ("600", "#D97706"),
                new ("700", "#B45309"),
                new ("800", "#92400E"),
                new ("900", "#78350F")
            },
        },
        new PaletteColor() {
            Name = "yellow",
            Variants = new List<ColorVariant> () {
                new ("50", "#FEFCE8"),
                new ("100", "#FEF9C3"),
                new ("200", "#FEF08A"),
                new ("300", "#FDE047"),
                new ("400", "#FACC15"),
                new ("500", "#EAB308"),
                new ("600", "#CA8A04"),
                new ("700", "#A16207"),
                new ("800", "#854D0E"),
                new ("900", "#713F12")
            },
        },
        new PaletteColor() {
            Name = "lime",
            Variants = new List<ColorVariant> () {
                new ("50", "#F7FEE7"),
                new ("100", "#ECFCCB"),
                new ("200", "#D9F99D"),
                new ("300", "#BEF264"),
                new ("400", "#A3E635"),
                new ("500", "#84CC16"),
                new ("600", "#65A30D"),
                new ("700", "#4D7C0F"),
                new ("800", "#3F6212"),
                new ("900", "#365314")
            },
        },
        new PaletteColor() {
            Name = "green",
            Variants = new List<ColorVariant> () {
                new ("50", "#F0FDF4"),
                new ("100", "#DCFCE7"),
                new ("200", "#BBF7D0"),
                new ("300", "#86EFAC"),
                new ("400", "#4ADE80"),
                new ("500", "#22C55E"),
                new ("600", "#16A34A"),
                new ("700", "#15803D"),
                new ("800", "#166534"),
                new ("900", "#14532D")
            },
        },
        new PaletteColor() {
            Name = "emerald",
            Variants = new List<ColorVariant> () {
                new ("50", "#ECFDF5"),
                new ("100", "#D1FAE5"),
                new ("200", "#A7F3D0"),
                new ("300", "#6EE7B7"),
                new ("400", "#34D399"),
                new ("500", "#10B981"),
                new ("600", "#059669"),
                new ("700", "#047857"),
                new ("800", "#065F46"),
                new ("900", "#064E3B")
            },
        },
        new PaletteColor() {
            Name = "teal",
            Variants = new List<ColorVariant> () {
                new ("50", "#F0FDFA"),
                new ("100", "#CCFBF1"),
                new ("200", "#99F6E4"),
                new ("300", "#5EEAD4"),
                new ("400", "#2DD4BF"),
                new ("500", "#14B8A6"),
                new ("600", "#0D9488"),
                new ("700", "#0F766E"),
                new ("800", "#115E59"),
                new ("900", "#134E4A")
            },
        },
        new PaletteColor() {
            Name = "cyan",
            Variants = new List<ColorVariant> () {
                new ("50", "#ECFEFF"),
                new ("100", "#CFFAFE"),
                new ("200", "#A5F3FC"),
                new ("300", "#67E8F9"),
                new ("400", "#22D3EE"),
                new ("500", "#06B6D4"),
                new ("600", "#0891B2"),
                new ("700", "#0E7490"),
                new ("800", "#155E75"),
                new ("900", "#164E63")
            },
        },
        new PaletteColor() {
            Name = "lightBlue",
            Variants = new List<ColorVariant> () {
                new ("50", "#F0F9FF"),
                new ("100", "#E0F2FE"),
                new ("200", "#BAE6FD"),
                new ("300", "#7DD3FC"),
                new ("400", "#38BDF8"),
                new ("500", "#0EA5E9"),
                new ("600", "#0284C7"),
                new ("700", "#0369A1"),
                new ("800", "#075985"),
                new ("900", "#0C4A6E")
            },
        },
        new PaletteColor() {
            Name = "blue",
            Variants = new List<ColorVariant> () {
                new ("50", "#EFF6FF"),
                new ("100", "#DBEAFE"),
                new ("200", "#BFDBFE"),
                new ("300", "#93C5FD"),
                new ("400", "#60A5FA"),
                new ("500", "#3B82F6"),
                new ("600", "#2563EB"),
                new ("700", "#1D4ED8"),
                new ("800", "#1E40AF"),
                new ("900", "#1E3A8A")
            },
        },
        new PaletteColor() {
            Name = "indigo",
            Variants = new List<ColorVariant> () {
                new ("50", "#EEF2FF"),
                new ("100", "#E0E7FF"),
                new ("200", "#C7D2FE"),
                new ("300", "#A5B4FC"),
                new ("400", "#818CF8"),
                new ("500", "#6366F1"),
                new ("600", "#4F46E5"),
                new ("700", "#4338CA"),
                new ("800", "#3730A3"),
                new ("900", "#312E81")
            },
        },
        new PaletteColor() {
            Name = "violet",
            Variants = new List<ColorVariant> () {
                new ("50", "#F5F3FF"),
                new ("100", "#EDE9FE"),
                new ("200", "#DDD6FE"),
                new ("300", "#C4B5FD"),
                new ("400", "#A78BFA"),
                new ("500", "#8B5CF6"),
                new ("600", "#7C3AED"),
                new ("700", "#6D28D9"),
                new ("800", "#5B21B6"),
                new ("900", "#4C1D95")
            },
        },
        new PaletteColor() {
            Name = "purple",
            Variants = new List<ColorVariant> () {
                new ("50", "#FAF5FF"),
                new ("100", "#F3E8FF"),
                new ("200", "#E9D5FF"),
                new ("300", "#D8B4FE"),
                new ("400", "#C084FC"),
                new ("500", "#A855F7"),
                new ("600", "#9333EA"),
                new ("700", "#7E22CE"),
                new ("800", "#6B21A8"),
                new ("900", "#581C87")
            },
        },
        new PaletteColor() {
            Name = "fuchsia",
            Variants = new List<ColorVariant> () {
                new ("50", "#FDF4FF"),
                new ("100", "#FAE8FF"),
                new ("200", "#F5D0FE"),
                new ("300", "#F0ABFC"),
                new ("400", "#E879F9"),
                new ("500", "#D946EF"),
                new ("600", "#C026D3"),
                new ("700", "#A21CAF"),
                new ("800", "#86198F"),
                new ("900", "#701A75")
            },
        },
        new PaletteColor() {
            Name = "pink",
            Variants = new List<ColorVariant> () {
                new ("50", "#FDF2F8"),
                new ("100", "#FCE7F3"),
                new ("200", "#FBCFE8"),
                new ("300", "#F9A8D4"),
                new ("400", "#F472B6"),
                new ("500", "#EC4899"),
                new ("600", "#DB2777"),
                new ("700", "#BE185D"),
                new ("800", "#9D174D"),
                new ("900", "#831843")
            },
        },
        new PaletteColor() {
            Name = "rose",
            Variants = new List<ColorVariant> () {
                new ("50", "#FFF1F2"),
                new ("100", "#FFE4E6"),
                new ("200", "#FECDD3"),
                new ("300", "#FDA4AF"),
                new ("400", "#FB7185"),
                new ("500", "#F43F5E"),
                new ("600", "#E11D48"),
                new ("700", "#BE123C"),
                new ("800", "#9F1239"),
                new ("900", "#881337")
            }
        }
    };
}