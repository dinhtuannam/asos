'use client';
import * as React from 'react';
import { X } from 'lucide-react';
import { Badge } from '@/components/ui/badge';
import { Command, CommandGroup, CommandItem, CommandList } from '@/components/ui/command';
import { Command as CommandPrimitive } from 'cmdk';

type MultiSelectProps = {
    options: SelectOption[];
    defaultValue?: string[];
    placeHolder?: string;
    onChange: (option: SelectOption[]) => void;
};

export default function MultiSelect({
    options,
    defaultValue = [],
    placeHolder = 'Vui lòng chọn ...',
    onChange,
}: MultiSelectProps) {
    const inputRef = React.useRef<HTMLInputElement>(null);
    const [open, setOpen] = React.useState(false);
    const initialSelected = options.filter((option) => defaultValue.some((item) => option.value === item));
    const [selected, setSelected] = React.useState<SelectOption[]>(initialSelected);
    const [inputValue, setInputValue] = React.useState('');

    React.useEffect(() => {
        onChange(selected);
    }, [selected]);

    const handleUnselect = React.useCallback((option: SelectOption) => {
        setSelected((prev) => prev.filter((s) => s.value !== option.value));
    }, []);

    const handleSelect = React.useCallback((option: SelectOption) => {
        setInputValue('');
        setSelected((prev) => [...prev, option]);
    }, []);

    const handleKeyDown = React.useCallback((e: React.KeyboardEvent<HTMLDivElement>) => {
        const input = inputRef.current;
        if (input) {
            if (e.key === 'Delete' || e.key === 'Backspace') {
                if (input.value === '') {
                    setSelected((prev) => {
                        const newSelected = [...prev];
                        newSelected.pop();
                        return newSelected;
                    });
                }
            }

            if (e.key === 'Escape') {
                input.blur();
            }
        }
    }, []);

    const selectables = options.filter((option) => !selected.includes(option));

    return (
        <Command onKeyDown={handleKeyDown} className="overflow-visible bg-transparent">
            <div className="group rounded-md border border-input px-3 py-2 text-sm">
                <div className="flex flex-wrap gap-1">
                    {selected.map((option, key) => {
                        return (
                            <Badge key={key} variant="secondary" className="">
                                {option.label}
                                <button
                                    className="ml-1 rounded-full outline-none ring-offset-background focus:ring-2 focus:ring-ring focus:ring-offset-2"
                                    onKeyDown={(e) => {
                                        if (e.key === 'Enter') {
                                            handleUnselect(option);
                                        }
                                    }}
                                    onMouseDown={(e) => {
                                        e.preventDefault();
                                        e.stopPropagation();
                                    }}
                                    onClick={() => handleUnselect(option)}
                                >
                                    <X className="h-3 w-3 text-muted-foreground hover:text-foreground" />
                                </button>
                            </Badge>
                        );
                    })}

                    <CommandPrimitive.Input
                        ref={inputRef}
                        value={inputValue}
                        onValueChange={setInputValue}
                        onBlur={() => setOpen(false)}
                        onFocus={() => setOpen(true)}
                        placeholder={placeHolder}
                        className="ml-2 flex-1 bg-transparent outline-none placeholder:text-muted-foreground"
                    />
                </div>
            </div>
            <div className="relative mt-2">
                <CommandList>
                    {open && selectables.length > 0 ? (
                        <div className="absolute top-0 z-10 w-full rounded-md border bg-popover text-popover-foreground shadow-md outline-none animate-in">
                            <CommandGroup className="h-full overflow-auto">
                                {selectables.map((option, key) => {
                                    return (
                                        <CommandItem
                                            key={key}
                                            onMouseDown={(e) => {
                                                e.preventDefault();
                                                e.stopPropagation();
                                            }}
                                            onSelect={() => handleSelect(option)}
                                            className={'cursor-pointer'}
                                        >
                                            {option.label}
                                        </CommandItem>
                                    );
                                })}
                            </CommandGroup>
                        </div>
                    ) : null}
                </CommandList>
            </div>
        </Command>
    );
}
