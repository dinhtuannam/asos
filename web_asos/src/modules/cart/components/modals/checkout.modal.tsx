import React from 'react';
import { Input } from '@/components/ui/input';
import { Label } from '@/components/ui/label';
import { Button } from '@/components/ui/button';
import DrawerContainer from '@/components/container/drawer.container';
import { ModalType } from '@/enums/modal.enum';
import useModalContext from '@/hooks/useModal';
import useObjectState from '@/hooks/useObjectState';

export interface CheckoutInfo {
    receiverName: string;
    phone: string;
    address: string;
}

const CheckoutModal: React.FC = () => {
    const { modals, closeModal } = useModalContext();
    const modalState = modals[ModalType.Checkout];

    const [checkoutInfo, setCheckoutInfo] = useObjectState<CheckoutInfo>({
        receiverName: '',
        phone: '',
        address: '',
    });

    if (!modalState || !modalState.visible) return null;

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { id, value } = e.target;
        setCheckoutInfo(id as keyof CheckoutInfo, value);
    };

    const handleCheckout = () => {
        // Xử lý logic checkout ở đây
        console.log('Checkout info:', checkoutInfo);
        closeModal(ModalType.Checkout);
    };

    return (
        <DrawerContainer
            title="Checkout information"
            open={modalState.visible}
            onClose={() => closeModal(ModalType.Checkout)}
        >
            <div className={'grid items-start gap-4 px-4 md:w-[400px] sm:w-full'}>
                <div className="grid gap-2">
                    <Label htmlFor="receiverName">Receiver Name</Label>
                    <Input
                        id="receiverName"
                        value={checkoutInfo.receiverName}
                        onChange={handleInputChange}
                        placeholder="Enter receiver's full name"
                    />
                </div>
                <div className="grid gap-2">
                    <Label htmlFor="phone">Phone</Label>
                    <Input
                        id="phone"
                        value={checkoutInfo.phone}
                        onChange={handleInputChange}
                        placeholder="Enter phone number"
                    />
                </div>
                <div className="grid gap-2">
                    <Label htmlFor="address">Address</Label>
                    <Input
                        id="address"
                        value={checkoutInfo.address}
                        onChange={handleInputChange}
                        placeholder="Enter delivery address"
                    />
                </div>
                <Button className="bg-green-600 hover:bg-green-700" onClick={handleCheckout}>
                    Checkout
                </Button>
            </div>
        </DrawerContainer>
    );
};

export default CheckoutModal;
