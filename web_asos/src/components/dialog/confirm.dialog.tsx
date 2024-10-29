import {
    AlertDialog,
    AlertDialogCancel,
    AlertDialogContent,
    AlertDialogDescription,
    AlertDialogFooter,
    AlertDialogHeader,
    AlertDialogTitle,
} from '@/components/ui/alert-dialog';
import { Button } from '@/components/ui/button';

type ConfirmDialogProps = {
    visible: boolean;
    closeModal: () => void;
    onSubmit?: () => void;
    title?: string;
    description?: string;
    variant?: 'outline' | 'destructive' | 'default';
};

const ConfirmDialog: React.FC<ConfirmDialogProps> = ({
    visible,
    closeModal,
    onSubmit,
    title = 'Confirm dialog',
    description = 'Are you sure you want to perform this action?',
    variant = 'outline',
}) => {
    const handleChange = (isOpen: boolean) => {
        if (!isOpen) closeModal();
    };

    const handleSubmit = () => {
        if (onSubmit) onSubmit();
        closeModal();
    };

    return (
        <AlertDialog open={visible} onOpenChange={handleChange}>
            <AlertDialogContent>
                <AlertDialogHeader>
                    <AlertDialogTitle>{title}</AlertDialogTitle>
                    <AlertDialogDescription>{description}</AlertDialogDescription>
                </AlertDialogHeader>
                <AlertDialogFooter>
                    <AlertDialogCancel asChild>
                        <Button variant="outline">Cancel</Button>
                    </AlertDialogCancel>
                    <Button variant={variant} onClick={handleSubmit}>
                        Confirm
                    </Button>
                </AlertDialogFooter>
            </AlertDialogContent>
        </AlertDialog>
    );
};

export default ConfirmDialog;
