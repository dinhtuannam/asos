import React, { useEffect } from 'react';
import { Input } from '@/components/ui/input';
import { Label } from '@/components/ui/label';
import DrawerContainer from '@/components/container/drawer.container';
import { ModalType } from '@/enums/modal.enum';
import useModalContext from '@/hooks/useModal';
import useObjectState from '@/hooks/useObjectState';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select';
import { Switch } from '@/components/ui/switch';
import { useToast } from '@/components/ui/use-toast';
import useCaller from '@/hooks/useCaller';
import LoadingButton from '@/components/ui/loading-button';
import { RoleType, UserStatusType } from '@/enums/entity-type.enum';

export interface UpdateUserInfo {
    id: string;
    email: string;
    phone: string;
    fullname: string;
    address: string;
    isEmailConfirmed: boolean;
    statusId: string;
    roleId: string;
}

const initialState: UpdateUserInfo = {
    id: '',
    email: '',
    phone: '',
    fullname: '',
    address: '',
    isEmailConfirmed: false,
    statusId: '',
    roleId: '',
};

const UpdateUserModal: React.FC = () => {
    const { loading, callApi } = useCaller<any>();
    const { modals, closeModal } = useModalContext();
    const { toast } = useToast();
    const [updateUserInfo, setUpdateUserInfo, setInfo] = useObjectState<UpdateUserInfo>(initialState);

    const modalState = modals[ModalType.UpdateUser];

    useEffect(() => {
        if (modalState && modalState.visible && modalState.data) {
            setInfo({
                id: modalState.data.id || '',
                email: modalState.data.email || '',
                phone: modalState.data.phone || '',
                fullname: modalState.data.fullname || '',
                address: modalState.data.address || '',
                isEmailConfirmed: modalState.data.isEmailConfirmed || false,
                statusId: modalState.data.status?.id || '',
                roleId: modalState.data.role?.id || '',
            });
        }
    }, [modalState]);

    if (!modalState || !modalState.visible) return null;

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { id, value } = e.target;
        setUpdateUserInfo(id as keyof UpdateUserInfo, value);
    };

    const handleSelectChange = (key: keyof UpdateUserInfo, value: string) => {
        setUpdateUserInfo(key, value);
    };

    const handleSwitchChange = (checked: boolean) => {
        setUpdateUserInfo('isEmailConfirmed', checked);
    };

    const validateForm = (): boolean => {
        if (!updateUserInfo.fullname.trim()) {
            toast({ title: 'Error', description: 'Full name is required', variant: 'destructive' });
            return false;
        }

        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!emailRegex.test(updateUserInfo.email)) {
            toast({ title: 'Error', description: 'Invalid email address', variant: 'destructive' });
            return false;
        }

        const phoneRegex = /^\d{10}$/; // Assumes a 10-digit phone number
        if (!phoneRegex.test(updateUserInfo.phone)) {
            toast({
                title: 'Error',
                description: 'Invalid phone number (should be 10 digits)',
                variant: 'destructive',
            });
            return false;
        }

        if (!updateUserInfo.address.trim()) {
            toast({ title: 'Error', description: 'Address is required', variant: 'destructive' });
            return false;
        }

        if (!updateUserInfo.roleId) {
            toast({ title: 'Error', description: 'Please select a role', variant: 'destructive' });
            return false;
        }

        if (!updateUserInfo.statusId) {
            toast({ title: 'Error', description: 'Please select a status', variant: 'destructive' });
            return false;
        }

        return true;
    };

    const handleUpdateUser = async () => {
        if (validateForm()) {
            const result = await callApi(
                `/identity-service/api/User`,
                {
                    method: 'PUT',
                    body: updateUserInfo,
                },
                'User updated successfully',
            );
            if (result.succeeded) {
                closeModal(ModalType.UpdateUser);
                if (modalState.callback) {
                    modalState.callback();
                }
            }
        }
    };

    return (
        <DrawerContainer title="Update User" open={modalState.visible} onClose={() => closeModal(ModalType.UpdateUser)}>
            <div className={'grid items-start gap-4 px-4 md:w-[400px] sm:w-full'}>
                <div className="grid gap-2">
                    <Label htmlFor="fullname">Full Name</Label>
                    <Input
                        id="fullname"
                        value={updateUserInfo.fullname}
                        onChange={handleInputChange}
                        placeholder="Enter full name"
                    />
                </div>
                <div className="grid gap-2">
                    <Label htmlFor="email">Email</Label>
                    <Input
                        id="email"
                        type="email"
                        value={updateUserInfo.email}
                        onChange={handleInputChange}
                        placeholder="Enter email address"
                    />
                </div>
                <div className="grid gap-2">
                    <Label htmlFor="phone">Phone</Label>
                    <Input
                        id="phone"
                        value={updateUserInfo.phone}
                        onChange={handleInputChange}
                        placeholder="Enter phone number"
                    />
                </div>
                <div className="grid gap-2">
                    <Label htmlFor="address">Address</Label>
                    <Input
                        id="address"
                        value={updateUserInfo.address}
                        onChange={handleInputChange}
                        placeholder="Enter address"
                    />
                </div>
                <div className="grid gap-2">
                    <Label>Role</Label>
                    <Select
                        onValueChange={(value) => handleSelectChange('roleId', value)}
                        value={updateUserInfo.roleId}
                    >
                        <SelectTrigger>
                            <SelectValue placeholder="Select Role" />
                        </SelectTrigger>
                        <SelectContent>
                            <SelectItem value={RoleType.ADMIN}>Admin</SelectItem>
                            <SelectItem value={RoleType.CUSTOMER}>Customer</SelectItem>
                        </SelectContent>
                    </Select>
                </div>
                <div className="grid gap-2">
                    <Label>Status</Label>
                    <Select
                        onValueChange={(value) => handleSelectChange('statusId', value)}
                        value={updateUserInfo.statusId}
                    >
                        <SelectTrigger>
                            <SelectValue placeholder="Select Status" />
                        </SelectTrigger>
                        <SelectContent>
                            <SelectItem value={UserStatusType.ACTIVE}>Active</SelectItem>
                            <SelectItem value={UserStatusType.BANNED}>Banned</SelectItem>
                        </SelectContent>
                    </Select>
                </div>
                <div className="grid gap-2">
                    <div className="flex items-center justify-between">
                        <Label htmlFor="isEmailConfirmed" className="text-sm font-medium">
                            Email Confirmation
                        </Label>
                        <Switch
                            id="isEmailConfirmed"
                            checked={updateUserInfo.isEmailConfirmed}
                            onCheckedChange={handleSwitchChange}
                        />
                    </div>
                    <p className="text-sm text-gray-500">
                        {updateUserInfo.isEmailConfirmed ? 'Email is confirmed' : 'Email is not confirmed'}
                    </p>
                </div>
                <LoadingButton
                    className="mt-2"
                    onClick={handleUpdateUser}
                    isLoading={loading}
                    loadingText="Updating User..."
                >
                    Update User
                </LoadingButton>
            </div>
        </DrawerContainer>
    );
};

export default UpdateUserModal;
