import React, { useState, useEffect } from 'react';
import { Input } from '@/components/ui/input';
import { Label } from '@/components/ui/label';
import { Button } from '@/components/ui/button';
import DrawerContainer from '@/components/container/drawer.container';
import { ModalType } from '@/enums/modal.enum';
import useModalContext from '@/hooks/useModal';

const DetailOrderModal: React.FC = () => {
    const { modals, closeModal } = useModalContext();
    const DetailOrderModalData = modals[ModalType.DetailOrder];

    const [info, setInfo] = useState(DetailOrderModalData?.data || null);

    useEffect(() => {
        setInfo(DetailOrderModalData?.data || null);
    }, [DetailOrderModalData?.data]);

    if (!DetailOrderModalData || !DetailOrderModalData.visible) return null;

    return (
        <DrawerContainer
            title="Detail order"
            open={DetailOrderModalData.visible}
            onClose={() => closeModal(ModalType.DetailOrder)}
        >
            <div className={'grid items-start gap-4 px-4 md:w-[400px] sm:w-full'}>
                <div className="grid gap-2">
                    <Label htmlFor="email">Email</Label>
                    <Input
                        type="email"
                        id="email"
                        defaultValue={info?.email}
                        onChange={(e) => setInfo({ ...info, email: e.target.value })}
                    />
                </div>
                <div className="grid gap-2">
                    <Label htmlFor="username">Username</Label>
                    <Input
                        id="username"
                        defaultValue={info?.username}
                        onChange={(e) => setInfo({ ...info, username: e.target.value })}
                    />
                </div>
                <Button variant="secondary" onClick={() => closeModal('update')}>
                    Save changes
                </Button>
            </div>
        </DrawerContainer>
    );
};

export default DetailOrderModal;
