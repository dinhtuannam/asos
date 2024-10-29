import { useState } from 'react';
import { Input } from '@/components/ui/input';
import { Button } from '@/components/ui/button';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select';
import { Separator } from '@/components/ui/separator';
import { ChevronDown, X } from 'lucide-react';
import useModalContext from '@/hooks/useModal';
import { ModalType } from '@/enums/modal.enum';

function Checkout() {
    const { openModal } = useModalContext();
    const [promoCode, setPromoCode] = useState('');
    const [appliedPromoCode, setAppliedPromoCode] = useState('');
    const [selectedPoints, setSelectedPoints] = useState('0');
    const productDiscount = ['GIFTCODE', 'FREESHIP', 'LETSHAVEFUN'];

    const handleCheckout = () => {
        openModal(ModalType.Checkout);
    };

    const handleApplyPromoCode = () => {
        setAppliedPromoCode(promoCode);
    };

    const handleSelectDiscount = (value: string) => {
        setPromoCode(value);
    };

    const clearPromoCode = () => {
        setPromoCode('');
        setAppliedPromoCode('');
    };

    return (
        <div className="bg-white p-6 rounded-lg shadow">
            <div className="flex justify-between mb-2">
                <h2 className="text-xl font-bold">TOTAL</h2>
                <span>£13.87</span>
            </div>

            <Separator className="bg-gray-300 my-4" />

            <div className="space-y-2 mb-4">
                <div className="flex justify-between">
                    <span className="font-semibold">Sub-total</span>
                    <span>£14.87</span>
                </div>
                <div className="flex justify-between">
                    <span className="font-semibold">Delivery</span>
                    <span className="text-blue-600">£5.00</span>
                </div>
                {appliedPromoCode !== '' && (
                    <div className="flex justify-between">
                        <span className="font-semibold">Discount</span>
                        <span className="text-red-500">£4.00</span>
                    </div>
                )}
                {selectedPoints !== '0' && (
                    <div className="flex justify-between">
                        <span className="font-semibold">Point</span>
                        <span className="text-red-500">£2.00</span>
                    </div>
                )}
            </div>

            <Separator className="bg-gray-300 mb-2" />

            <div className="space-y-4 mb-4">
                <div>
                    <label htmlFor="promoCode" className="block text-sm font-medium text-gray-700 mb-1">
                        Promo Code
                    </label>
                    <div className="flex space-x-2">
                        <div className="relative flex-grow">
                            <Input
                                id="promoCode"
                                type="text"
                                value={promoCode}
                                onChange={(e) => setPromoCode(e.target.value)}
                                className="pr-8 w-full"
                                placeholder="Enter promo code"
                            />
                            {promoCode && (
                                <button
                                    onClick={clearPromoCode}
                                    className="absolute right-2 top-1/2 transform -translate-y-1/2 text-gray-400 hover:text-gray-600"
                                >
                                    <X size={16} />
                                </button>
                            )}
                        </div>
                        {productDiscount.length > 0 && (
                            <Select onValueChange={handleSelectDiscount}>
                                <SelectTrigger className="w-[40px] px-0">
                                    <ChevronDown className="h-4 w-4" />
                                </SelectTrigger>
                                <SelectContent>
                                    {productDiscount.map((discount) => (
                                        <SelectItem key={discount} value={discount}>
                                            {discount}
                                        </SelectItem>
                                    ))}
                                </SelectContent>
                            </Select>
                        )}
                        <Button onClick={handleApplyPromoCode}>Apply</Button>
                    </div>
                </div>
                <div>
                    <label htmlFor="points" className="block text-sm font-medium text-gray-700 mb-1">
                        Use Points
                    </label>
                    <Select value={selectedPoints} onValueChange={setSelectedPoints}>
                        <SelectTrigger className="w-full">
                            <SelectValue placeholder="Select points to use" />
                        </SelectTrigger>
                        <SelectContent>
                            <SelectItem value="0">Don't use points</SelectItem>
                            <SelectItem value="5">5 points</SelectItem>
                            <SelectItem value="10">10 points</SelectItem>
                            <SelectItem value="20">20 points</SelectItem>
                            <SelectItem value="50">50 points</SelectItem>
                            <SelectItem value="100">100 points</SelectItem>
                        </SelectContent>
                    </Select>
                </div>
            </div>

            <Button className="w-full text-white" variant={'default'} onClick={handleCheckout}>
                FILL INFORMATIONS
            </Button>
        </div>
    );
}

export default Checkout;
