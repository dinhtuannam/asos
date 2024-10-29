import { useState, useEffect, useRef } from 'react';
import { Separator } from '@/components/ui/separator';
import CartItem from '../components/items/cart-item';
import RecommendedItem from '../components/items/recommended-item';
import Checkout from '../components/items/checkout';
import CheckoutSkeleton from '../components/skeletons/checkout.skeleton';
import RecommendedItemSkeleton from '../components/skeletons/recommended-item.skeleton';
import CartItemSkeleton from '../components/skeletons/cart-item.skeleton';
import CheckoutModal from '../components/modals/checkout.modal';

const img1 =
    'https://images.asos-media.com/products/asos-design-essential-muscle-fit-t-shirt-in-grey/206460996-1-highrise';
const img2 =
    'https://images.asos-media.com/products/asos-design-essential-muscle-fit-t-shirt-in-white/203371683-1-white';

function CartPage() {
    const [isSticky, setIsSticky] = useState(false);
    const [isLoading, setIsLoading] = useState(true);
    const checkoutRef = useRef<HTMLDivElement>(null);
    const containerRef = useRef<HTMLDivElement>(null);

    useEffect(() => {
        const handleScroll = () => {
            if (checkoutRef.current && containerRef.current) {
                const containerTop = containerRef.current.getBoundingClientRect().top - 82;
                const containerBottom = containerRef.current.getBoundingClientRect().bottom;
                const checkoutHeight = checkoutRef.current.offsetHeight;

                if (containerTop <= 0 && containerBottom >= checkoutHeight) {
                    setIsSticky(true);
                } else {
                    setIsSticky(false);
                }
            }
        };

        window.addEventListener('scroll', handleScroll);
        return () => window.removeEventListener('scroll', handleScroll);
    }, []);

    useEffect(() => {
        // Simulate loading
        setTimeout(() => {
            setIsLoading(false);
        }, 1500);
    }, []);

    return (
        <div className="bg-gray-200 min-h-screen">
            <CheckoutModal />
            <div className="container mx-auto px-4 py-8" ref={containerRef}>
                <div className="flex flex-col lg:flex-row gap-8">
                    <div className="lg:w-2/3 bg-white p-6 rounded-lg shadow">
                        <div className="flex justify-between items-center mb-4">
                            <h1 className="text-2xl font-bold">MY BAG</h1>
                            <span className="text-sm text-gray-500">Items are reserved for 60 minutes</span>
                        </div>
                        <Separator className="bg-gray-300" />
                        <div className="space-y-6 mt-4">
                            {isLoading ? (
                                <>
                                    <CartItemSkeleton />
                                    <CartItemSkeleton />
                                </>
                            ) : (
                                <>
                                    <CartItem
                                        image={img1}
                                        title="ASOS DESIGN essential muscle fit t-shirt in grey"
                                        price="£6.37"
                                        originalPrice="£8.50"
                                        size="XS"
                                        quantity={1}
                                        color="HIGH-RISE"
                                    />
                                    <CartItem
                                        image={img2}
                                        title="ASOS DESIGN essential muscle fit t-shirt in white"
                                        price="£8.50"
                                        size="XS"
                                        quantity={1}
                                        isFastSelling={true}
                                        color="WHITE"
                                    />
                                </>
                            )}
                        </div>

                        <Separator className="bg-gray-300" />

                        <div className="mt-8">
                            <div className="flex justify-between items-center mb-4">
                                <h2 className="text-xl font-semibold">A LITTLE SOMETHING EXTRA?</h2>
                                <span className="text-sm text-gray-500">16 items</span>
                            </div>
                            <div className="flex space-x-4 overflow-x-auto pb-4">
                                {isLoading ? (
                                    <>
                                        <RecommendedItemSkeleton />
                                        <RecommendedItemSkeleton />
                                        <RecommendedItemSkeleton />
                                    </>
                                ) : (
                                    <>
                                        <RecommendedItem image={img1} title="ASOS DESIGN muscle vest in black" />
                                        <RecommendedItem image={img2} title="ASOS DESIGN muscle vest in white" />
                                        <RecommendedItem image={img1} title="COLLUSION ribbed vest in white" />
                                    </>
                                )}
                            </div>
                        </div>
                    </div>

                    <div className={`lg:w-1/3 ${isSticky ? 'lg:sticky lg:top-32 self-start' : ''}`} ref={checkoutRef}>
                        {isLoading ? <CheckoutSkeleton /> : <Checkout />}
                    </div>
                </div>
            </div>
        </div>
    );
}

export default CartPage;
