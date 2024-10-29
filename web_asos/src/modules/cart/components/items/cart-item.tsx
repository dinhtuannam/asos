import { X, Heart } from 'lucide-react';

interface CartItemProps {
    image: string;
    title: string;
    price: string;
    originalPrice?: string;
    size: string;
    quantity: number;
    isFastSelling?: boolean;
    color: string;
}

function CartItem({ image, title, price, originalPrice, isFastSelling, color }: CartItemProps) {
    return (
        <div className="flex items-start pb-6 border-b">
            <img src={image} alt={title} className="w-24 h-32 object-cover mr-4" />
            <div className="flex-grow">
                <div className="flex justify-between">
                    <div>
                        <p className="font-semibold text-lg">
                            {price}{' '}
                            {originalPrice && (
                                <span className="line-through text-gray-500 text-sm ml-2">{originalPrice}</span>
                            )}
                        </p>
                        <h3 className="text-sm text-gray-600 mb-1">{title}</h3>
                        {isFastSelling && (
                            <span className="bg-slate-500 text-white font-semibold tracking-wider text-xs px-2 py-1 rounded">
                                SELLING FAST
                            </span>
                        )}
                    </div>
                    <X className="cursor-pointer" />
                </div>
                <div className="mt-2 flex items-center space-x-4">
                    <div className="flex flex-row items-center space-x-2 text-sm ">
                        <span className=" text-gray-500">{color}</span>
                        <span className="">XS</span>
                        <span className="">Qty 1</span>
                    </div>
                </div>
                <button className="flex items-center text-sm mt-2 text-gray-500">
                    <Heart size={16} className="mr-1" /> Save for later
                </button>
            </div>
        </div>
    );
}

export default CartItem;
