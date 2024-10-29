import { useState, useEffect } from 'react';
import { Trash2 } from 'lucide-react';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select';
import { Card, CardContent } from '@/components/ui/card';
import { Link } from 'react-router-dom';
import WishlistSkeleton from '../components/wishlist.skeleton';

const img1 =
    '//images.asos-media.com/products/hugo-red-diblostee-oversized-t-shirt-in-black-with-sleeve-placement-floral-print/206765291-1-black?$n_480w$&wid=476&fit=constrain';

const wishlistItems = [
    {
        id: 1,
        title: 'HUGO Red diblosee oversized t-shirt in black with sleeve placement floral print',
        price: '£83.00',
    },
    {
        id: 2,
        title: 'ASOS DESIGN cotton bend knitted jumper with contrast collar and cuff in black and stone',
        price: '£37.00',
    },
    {
        id: 3,
        title: 'Polo Ralph Lauren Heritage straight fit jeans in dark wash',
        price: '£169.00',
    },
    {
        id: 4,
        title: 'Crocs unisex echo sliders in black',
        price: '£34.80',
        originalPrice: '£50.00',
    },
    {
        id: 5,
        title: 'Crocs unisex echo sliders in black',
        price: '£34.80',
        originalPrice: '£50.00',
    },
];

function WishlistPage() {
    const [sortBy, setSortBy] = useState('recently-added');
    const [isLoading, setIsLoading] = useState(true);
    const [items, setItems] = useState(wishlistItems);

    useEffect(() => {
        setIsLoading(true);
        setTimeout(() => {
            setItems(wishlistItems);
            setIsLoading(false);
        }, 1500);
    }, []);

    const handleDelete = (id: number) => {
        console.log(`Deleting item with id: ${id}`);
        setItems(items.filter((item) => item.id !== id));
    };

    return (
        <div className="bg-white min-h-screen py-8">
            <div className="container mx-auto px-4">
                <h1 className="text-2xl font-bold mb-6 text-center">Saved Items</h1>
                <div className="flex justify-between items-center mb-6">
                    <Select value={sortBy} onValueChange={setSortBy}>
                        <SelectTrigger className="w-[200px]">
                            <SelectValue placeholder="Sort by" />
                        </SelectTrigger>
                        <SelectContent>
                            <SelectItem value="recently-added">Recently added</SelectItem>
                            <SelectItem value="price-high-low">Price: High to Low</SelectItem>
                            <SelectItem value="price-low-high">Price: Low to High</SelectItem>
                        </SelectContent>
                    </Select>
                    <span className="text-sm text-gray-500">{items.length} items</span>
                </div>
                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
                    {isLoading ? (
                        <WishlistSkeleton />
                    ) : (
                        items.map((item) => (
                            <Card key={item.id} className="relative group">
                                <CardContent className="p-4">
                                    <div className="absolute top-2 right-2 p-2 bg-white rounded-full cursor-pointer z-10">
                                        <Trash2
                                            size={20}
                                            className="text-gray-400 group-hover:text-red-500 transition-colors"
                                            onClick={() => handleDelete(item.id)}
                                        />
                                    </div>
                                    <Link
                                        to={'/'}
                                        className="block aspect-w-3 aspect-h-4 mb-4 cursor-pointer overflow-hidden"
                                    >
                                        <img
                                            src={img1}
                                            alt={item.title}
                                            className="w-full h-full object-cover transition-transform duration-300 group-hover:scale-105"
                                        />
                                    </Link>
                                    <div className="relative z-10 bg-white">
                                        <h3 className="text-sm tracking-wider mb-2 line-clamp-2 pt-2">{item.title}</h3>
                                        <div className="flex justify-between items-center">
                                            <span className="font-bold">{item.price}</span>
                                            {item.originalPrice && (
                                                <span className="text-sm line-through text-gray-500">
                                                    {item.originalPrice}
                                                </span>
                                            )}
                                        </div>
                                    </div>
                                </CardContent>
                            </Card>
                        ))
                    )}
                </div>
            </div>
        </div>
    );
}

export default WishlistPage;
