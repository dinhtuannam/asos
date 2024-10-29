import { cn } from '@/lib/utils';

import { Sheet, SheetContent, SheetDescription, SheetHeader, SheetTitle } from '@/components/ui/sheet';

type SheetContainerProps = {
    children: React.ReactNode;
    title?: string;
    description?: string;
    open: boolean;
    onClose: () => void;
    className?: string;
    side?: 'top' | 'right' | 'bottom' | 'left';
};

export default function SheetContainer({
    children,
    title,
    description,
    open,
    onClose,
    className,
    side = 'left', // Set default to 'left'
}: SheetContainerProps) {
    const handleChange = (isOpen: boolean) => {
        if (!isOpen) onClose();
    };

    return (
        <Sheet open={open} onOpenChange={handleChange}>
            <SheetContent side={side} className={cn(className)}>
                <SheetHeader>
                    {title && <SheetTitle>{title}</SheetTitle>}
                    {description && <SheetDescription>{description}</SheetDescription>}
                </SheetHeader>
                {children}
            </SheetContent>
        </Sheet>
    );
}
