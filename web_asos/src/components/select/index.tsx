import * as React from 'react';
import { Select, SelectContent, SelectGroup, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select';
import { cn } from '@/lib/utils';

type SelectProps = {
    options: SelectOption[];
    defaultValue?: string;
    placeHolder?: string;
    triggerStyle?: string;
    itemStyle?: string;
    onChange: (option: SelectOption) => void;
};

export function SelectDemo({
    options,
    defaultValue,
    triggerStyle = 'w-44',
    itemStyle = '',
    placeHolder = 'Vui lòng chọn ...',
    onChange,
}: SelectProps) {
    const [selected, setSelected] = React.useState<SelectOption | null>(() => {
        const initial = options.find((x) => x.value == defaultValue);
        if (initial) return initial;
        return null;
    });

    React.useEffect(() => {
        if (selected) onChange(selected);
    }, [selected]);

    const handleSelect = (value: String) => {
        const option = options.find((x) => x.value === value);
        if (!option) return;
        setSelected(option);
    };

    return (
        <Select onValueChange={(value) => handleSelect(value)}>
            <SelectTrigger className={cn(triggerStyle)}>
                <SelectValue placeholder={placeHolder} />
            </SelectTrigger>
            <SelectContent>
                <SelectGroup>
                    {options.map((item, index) => {
                        return (
                            <SelectItem value={item.value} key={index} className={cn(itemStyle)}>
                                {item.label}
                            </SelectItem>
                        );
                    })}
                </SelectGroup>
            </SelectContent>
        </Select>
    );
}
